﻿// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Legacy Shaders/Self-Illumin/Parallax Diffuse (Atlas)" {
Properties {
		_ColorMap("Main Color", 2D) = "white" {}
		_Parallax("Height", 2D) = "black" {}
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Illum("Illumin (A)", 2D) = "white" {}
		_BumpMap("Normalmap", 2D) = "bump" {}
		_ParallaxMap("Heightmap (A)", 2D) = "black" {}
		_Emission("Emission (Lightmapper)", Float) = 1.0
		_MaxMipLevel("Max Mip Level", float) = 3
		_AtlasHeight("Atlas Height", float) = 2048
		_AtlasWidth("Atlas Width", float) = 2048
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 500

CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		#pragma target 3.0
		float _MaxMipLevel;
		float _AtlasHeight;
		float _AtlasWidth;
		float4 pickUVlod(float2 uv, float4 bounds)
		{
			float2 relativeSize = frac(bounds.zw);
			float2 scale = floor(bounds.zw) / 1000;
			float2 fracuv = frac(uv.xy * scale) * relativeSize;
			float2 uv_atlas = fracuv + bounds.xy;
			float2 subTextureSize = relativeSize * scale * float2(_AtlasWidth, _AtlasHeight);
			float2 dx = ddx(uv * subTextureSize.x);
			float2 dy = ddy(uv * subTextureSize.y);
			int d = max(dot(dx, dx), dot(dy, dy));
			
			const float rangeClamp = pow(2, _MaxMipLevel * 2);
			d = clamp(d, 1.0, rangeClamp);
			
			float mipLevel = 0.5 * log2(d);
			mipLevel = floor(mipLevel);
			
			return float4(uv_atlas, 0, mipLevel);
		}

		float4 tex2Datlas(sampler2D tex, float2 uv, float4 atlas)
		{
			return tex2Dlod(tex, pickUVlod(uv, atlas));
		}

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _ParallaxMap;
		sampler2D _Illum;
		sampler2D _ColorMap;
		sampler2D _Parallax;
		fixed _Emission;
		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_Illum;
			float3 viewDir;
			float4 atlas;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.atlas = v.texcoord2;
		}


		void surf(Input IN, inout SurfaceOutput o)
		{
			half h = tex2Datlas(_ParallaxMap, IN.uv_BumpMap, IN.atlas).w;
			float2 offset = ParallaxOffset(h, tex2Datlas(_Parallax, IN.uv_MainTex, IN.atlas).x, IN.viewDir);
			IN.uv_MainTex += offset;
			IN.uv_BumpMap += offset;
			IN.uv_Illum += offset;
			
			fixed4 c = tex2Datlas(_MainTex, IN.uv_MainTex, IN.atlas) * tex2Datlas(_ColorMap, IN.uv_MainTex, IN.atlas);
			o.Albedo = c.rgb;
			o.Emission = c.rgb * tex2Datlas(_Illum, IN.uv_Illum, IN.atlas).a;
#if defined (UNITY_PASS_META)
			o.Emission *= _Emission.rrr;
#endif
			o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2Datlas(_BumpMap, IN.uv_BumpMap, IN.atlas));
		}


ENDCG
}
FallBack "Legacy Shaders/Self-Illumin/Bumped Diffuse"
CustomEditor "LegacyIlluminShaderGUI"

}
