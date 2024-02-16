using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data.Settings;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.DestructableObjects
{
    [RequireComponent(typeof(AudioSource))]
    public class DestructableObjectDeath : MonoBehaviour, IDeath
    //, IProgressReader
    {
        [SerializeField] private GameObject _solid;
        [SerializeField] private GameObject _broken;
        [SerializeField] private DestructableTypeId _typeId;

        private AudioSource _audioSource;
        private float _deathDelay = 50f;
        private bool _isBroken = false;
        private List<Rigidbody> _parts;
        private SettingsData _settingsData;
        private float _volume;
        private WaitForSeconds _waitForSeconds;

        public event Action Died;

        private void Awake()
        {
            _solid.SetActive(true);
            _broken.SetActive(false);
            _parts = new List<Rigidbody>(_broken.transform.childCount);
            _audioSource = GetComponent<AudioSource>();

            _settingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;

            for (int i = 0; i < _broken.transform.childCount; i++)
                _parts.Add(_broken.transform.GetChild(i).GetComponent<Rigidbody>());

            _waitForSeconds = new WaitForSeconds(_deathDelay);
        }

        private void OnEnable()
        {
            if (_settingsData == null)
                return;

            _settingsData.SoundSwitchChanged += SwitchChanged;
            _settingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
        }

        private void OnDisable()
        {
            if (_settingsData == null)
                return;

            _settingsData.SoundSwitchChanged -= SwitchChanged;
            _settingsData.SoundVolumeChanged -= VolumeChanged;
        }

        public void Die()
        {
            _solid.SetActive(false);
            _broken.SetActive(true);
            Destroy(GetComponent<BoxCollider>());
            Destroy(_solid.GetComponentInChildren<BoxCollider>());

            if (_isBroken == false)
                foreach (Rigidbody part in _parts)
                    part.AddForce(part.gameObject.transform.forward * 5f, ForceMode.Impulse);

            PlaySound();
            _isBroken = true;

            StartCoroutine(DestroyTimer());
        }

        private void PlaySound()
        {
            switch (_typeId)
            {
                case DestructableTypeId.WoodenBox:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.DestructionWoodenBox),
                        transform: transform, _volume, _audioSource);
                    break;
                case DestructableTypeId.Concrete:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.DestructionConcrete),
                        transform: transform, _volume, _audioSource);
                    break;
            }
        }

        private IEnumerator DestroyTimer()
        {
            yield return _waitForSeconds;
            Destroy(gameObject);
        }

        private void VolumeChanged() =>
            _volume = _settingsData.SoundVolume;

        private void SwitchChanged() =>
            _volume = _settingsData.SoundOn ? _settingsData.SoundVolume : Constants.Zero;
    }
}