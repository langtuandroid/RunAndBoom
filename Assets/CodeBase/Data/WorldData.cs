using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public string Level { get; private set; }
        public string Section { get; private set; }

        public event Action<string> SectionChanged;

        public WorldData(string level)
        {
            Level = level;
            SectionChanged += ChangeSection;
        }

        private void ChangeSection(string section) => 
            Section = section;
    }
}