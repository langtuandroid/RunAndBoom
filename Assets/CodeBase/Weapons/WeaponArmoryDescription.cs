namespace CodeBase.Weapons
{
    public class WeaponArmoryDescription
    {
        public string Name { get; private set; }

        public int MainFireCost { get; private set; }

        // public int SecondaryFireCost { get; private set; }
        public int MainFireDamage { get; private set; }

        // public int SecondaryFireDamage { get; private set; }
        public float MainFireCooldown { get; private set; }

        // public float SecondaryFireCooldown { get; private set; }
        public int MainFireBarrels { get; private set; }

        // public int SecondaryFireBarrels { get; private set; }
        public float MainFireRange { get; private set; }

        // public float SecondaryFireRange { get; private set; }
        public float MainFireBulletSpeed { get; private set; }

        // public float SecondaryFireBulletSpeed { get; private set; }
        public float MainFireRotatingSpeed { get; private set; }
        // public float SecondaryFireRotatingSpeed { get; private set; }

        public WeaponArmoryDescription(string name, int mainFireCost,
            // int secondaryFireCost, 
            int mainFireDamage,
            // int secondaryFireDamage, 
            float mainFireCooldown,
            // float secondaryFireCooldown, 
            int mainFireBarrels,
            // int secondaryFireBarrels, 
            float mainFireRange,
            // float secondaryFireRange, 
            float mainFireBulletSpeed,
            // , float secondaryFireBulletSpeed
            float mainFireRotatingSpeed
            // , float secondaryFireRotatingSpeed
        )
        {
            Name = name;
            MainFireCost = mainFireCost;
            // SecondaryFireCost = secondaryFireCost;
            MainFireDamage = mainFireDamage;
            // SecondaryFireDamage = secondaryFireDamage;
            MainFireCooldown = mainFireCooldown;
            // SecondaryFireCooldown = secondaryFireCooldown;
            MainFireBarrels = mainFireBarrels;
            // SecondaryFireBarrels = secondaryFireBarrels;
            MainFireRange = mainFireRange;
            // SecondaryFireRange = secondaryFireRange;
            MainFireBulletSpeed = mainFireBulletSpeed;
            // SecondaryFireBulletSpeed = secondaryFireBulletSpeed;
            MainFireRotatingSpeed = mainFireRotatingSpeed;
            // SecondaryFireRotatingSpeed = secondaryFireRotatingSpeed;
        }
    }
}