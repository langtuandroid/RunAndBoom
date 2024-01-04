using System;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class HealthState
    {
        public float CurrentHp;
        public float MaxHp;

        public HealthState(SceneId sceneId)
        {
            if (sceneId == SceneId.Level_1)
            {
                MaxHp = Constants.InitialMaxHp;
                CurrentHp = MaxHp;
            }
        }
    }
}