using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Logic
{
    public static class LogicExtensions
    {
        public static void StopHero(this GameObject hero)
        {
            if (hero.GetComponent<HeroShooting>() != null)
            {
                hero.GetComponent<HeroShooting>().TurnOff();
                // TODO hero doesn't fall down
                // hero.GetComponent<HeroMovement>().TurnOff();
                hero.GetComponent<HeroRotating>().TurnOff();
                hero.GetComponent<HeroReloading>().TurnOff();
                hero.GetComponentInChildren<HeroWeaponSelection>().TurnOff();
                hero.GetComponentInChildren<PlayTimer>().enabled = false;
            }
        }

        public static void ResumeHero(this GameObject hero)
        {
            if (hero.GetComponent<HeroShooting>() != null)
            {
                hero.GetComponent<HeroShooting>().TurnOn();
                hero.GetComponent<HeroMovement>().TurnOn();
                hero.GetComponent<HeroRotating>().TurnOn();
                hero.GetComponent<HeroReloading>().TurnOn();
                hero.GetComponentInChildren<HeroWeaponSelection>().TurnOn();
                hero.GetComponentInChildren<PlayTimer>().enabled = true;
            }
        }
    }
}