using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Projectiles;
using CodeBase.Projectiles.Hit;
using CodeBase.Projectiles.Movement;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Hits;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.ShotVfxs;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public class PoolService : IPoolService
    {
        private const int InitialCapacity = 10;
        private const int AdditionalCount = 2;

        private IAssets _assets;
        private IStaticDataService _staticDataService;
        private Dictionary<string, List<GameObject>> _heroProjectiles;
        private Dictionary<string, List<GameObject>> _enemyProjectiles;
        private Dictionary<string, List<GameObject>> _shotVfxs;
        private Transform _enemyProjectilesRoot;
        private Transform _heroProjectilesRoot;
        private Transform _shotVfxsRoot;

        public PoolService(IAssets assets, IStaticDataService staticDataService)
        {
            _assets = assets;
            _staticDataService = staticDataService;
        }

        public void GenerateObjects()
        {
            CreateRoots();
        }

        private async void CreateRoots()
        {
            GameObject root = await _assets.Instantiate(AssetAddresses.EnemyProjectilesRoot);
            _enemyProjectilesRoot = root.transform;

            root = await _assets.Instantiate(AssetAddresses.HeroProjectilesRoot);
            _heroProjectilesRoot = root.transform;

            root = await _assets.Instantiate(AssetAddresses.ShotVfxsRoot);
            _shotVfxsRoot = root.transform;

            GenerateEnemyProjectiles();
            GenerateHeroProjectiles();
            GenerateShotVfxs();
        }

        private async void GenerateEnemyProjectiles()
        {
            _enemyProjectiles = new Dictionary<string, List<GameObject>>();
            List<GameObject> gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(ProjectileTypeId.PistolBullet);
                GameObject projectile = await _assets.Instantiate(AssetAddresses.PistolBullet, _enemyProjectilesRoot);
                projectile.GetComponent<BulletMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _enemyProjectiles.Add(EnemyWeaponTypeId.Pistol.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(ProjectileTypeId.Shot);
                GameObject projectile = await _assets.Instantiate(AssetAddresses.Shot, _enemyProjectilesRoot);
                projectile.GetComponent<ShotMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _enemyProjectiles.Add(EnemyWeaponTypeId.Shotgun.ToString(), gameObjects);
        }

        private async void GenerateHeroProjectiles()
        {
            _heroProjectiles = new Dictionary<string, List<GameObject>>();
            List<GameObject> gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(ProjectileTypeId.Grenade);
                BlastStaticData blastStaticData = _staticDataService.ForBlast(BlastTypeId.Grenade);
                TrailStaticData trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
                GameObject projectile = await _assets.Instantiate(AssetAddresses.Grenade, _heroProjectilesRoot);
                projectile.GetComponent<GrenadeMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                projectile.GetComponentInChildren<ProjectileBlast>().Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
                projectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _heroProjectiles.Add(HeroWeaponTypeId.GrenadeLauncher.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(ProjectileTypeId.RpgRocket);
                BlastStaticData blastStaticData = _staticDataService.ForBlast(BlastTypeId.RpgRocket);
                TrailStaticData trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
                GameObject projectile = await _assets.Instantiate(AssetAddresses.RpgRocket, _heroProjectilesRoot);
                projectile.GetComponent<ShotMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                projectile.GetComponentInChildren<ProjectileBlast>().Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
                projectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _heroProjectiles.Add(HeroWeaponTypeId.RPG.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(ProjectileTypeId.RocketLauncherRocket);
                BlastStaticData blastStaticData = _staticDataService.ForBlast(BlastTypeId.RocketLauncherRocket);
                TrailStaticData trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
                GameObject projectile = await _assets.Instantiate(AssetAddresses.RocketLauncherRocket, _heroProjectilesRoot);
                projectile.GetComponent<ShotMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                projectile.GetComponentInChildren<ProjectileBlast>().Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
                projectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _heroProjectiles.Add(HeroWeaponTypeId.RocketLauncher.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(ProjectileTypeId.Bomb);
                BlastStaticData blastStaticData = _staticDataService.ForBlast(BlastTypeId.Bomb);
                TrailStaticData trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
                GameObject projectile = await _assets.Instantiate(AssetAddresses.Bomb, _heroProjectilesRoot);
                projectile.GetComponent<BombMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                projectile.GetComponentInChildren<ProjectileBlast>().Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
                projectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _heroProjectiles.Add(HeroWeaponTypeId.Mortar.ToString(), gameObjects);
        }

        private async void GenerateShotVfxs()
        {
            _shotVfxs = new Dictionary<string, List<GameObject>>();
            List<GameObject> gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject shotVfx = await _assets.Instantiate(AssetAddresses.GrenadeMuzzleFire, _shotVfxsRoot);
                shotVfx.SetActive(false);
                gameObjects.Add(shotVfx);
            }

            _shotVfxs.Add(ShotVfxTypeId.Grenade.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject shotVfx = await _assets.Instantiate(AssetAddresses.RocketMuzzleFire, _shotVfxsRoot);
                shotVfx.SetActive(false);
                gameObjects.Add(shotVfx);
            }

            _shotVfxs.Add(ShotVfxTypeId.RpgRocket.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject shotVfx = await _assets.Instantiate(AssetAddresses.RocketMuzzleBlue, _shotVfxsRoot);
                shotVfx.SetActive(false);
                gameObjects.Add(shotVfx);
            }

            _shotVfxs.Add(ShotVfxTypeId.RocketLauncherRocket.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject shotVfx = await _assets.Instantiate(AssetAddresses.BombMuzzle, _shotVfxsRoot);
                shotVfx.SetActive(false);
                gameObjects.Add(shotVfx);
            }

            _shotVfxs.Add(ShotVfxTypeId.Bomb.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject shotVfx = await _assets.Instantiate(AssetAddresses.BulletMuzzleFire, _shotVfxsRoot);
                shotVfx.SetActive(false);
                gameObjects.Add(shotVfx);
            }

            _shotVfxs.Add(ShotVfxTypeId.Bullet.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject shotVfx = await _assets.Instantiate(AssetAddresses.RocketMuzzleFire, _shotVfxsRoot);
                shotVfx.SetActive(false);
                gameObjects.Add(shotVfx);
            }

            _shotVfxs.Add(ShotVfxTypeId.Shot.ToString(), gameObjects);
        }

        public GameObject GetEnemyProjectile(string name) =>
            GetGameObject(name, _enemyProjectiles, _enemyProjectilesRoot);

        public GameObject GetHeroProjectile(string name) =>
            GetGameObject(name, _heroProjectiles, _heroProjectilesRoot);

        public GameObject GetShotVfx(ShotVfxTypeId typeId)
        {
            GameObject gameObject = null;

            switch (typeId)
            {
                case ShotVfxTypeId.Grenade:
                    gameObject = GetGameObject(ShotVfxTypeId.Grenade.ToString(), _shotVfxs, _shotVfxsRoot);
                    break;
                case ShotVfxTypeId.RocketLauncherRocket:
                    gameObject = GetGameObject(ShotVfxTypeId.RocketLauncherRocket.ToString(), _shotVfxs, _shotVfxsRoot);
                    break;
                case ShotVfxTypeId.RpgRocket:
                    gameObject = GetGameObject(ShotVfxTypeId.RpgRocket.ToString(), _shotVfxs, _shotVfxsRoot);
                    break;
                case ShotVfxTypeId.Bomb:
                    gameObject = GetGameObject(ShotVfxTypeId.Bomb.ToString(), _shotVfxs, _shotVfxsRoot);
                    break;
                case ShotVfxTypeId.Bullet:
                    gameObject = GetGameObject(ShotVfxTypeId.Bullet.ToString(), _shotVfxs, _shotVfxsRoot);
                    break;
                case ShotVfxTypeId.Shot:
                    gameObject = GetGameObject(ShotVfxTypeId.Shot.ToString(), _shotVfxs, _shotVfxsRoot);
                    break;
            }

            return gameObject;
        }

        public void ReturnEnemyProjectile(GameObject gameObject) =>
            ReturnGameObject(gameObject, _enemyProjectilesRoot);

        public void ReturnHeroProjectile(GameObject gameObject) =>
            ReturnGameObject(gameObject, _heroProjectilesRoot);

        public void ReturnShotVfx(GameObject gameObject) =>
            ReturnGameObject(gameObject, _shotVfxsRoot);

        private void ReturnGameObject(GameObject gameObject, Transform parent)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(parent);
        }

        private GameObject GetGameObject(string name, Dictionary<string, List<GameObject>> dictionary, Transform parent)
        {
            dictionary.TryGetValue(name, out List<GameObject> list);
            GameObject gameObject = null;

            if (list != null)
            {
                gameObject = list.FirstOrDefault(it => it.activeInHierarchy == false);

                if (gameObject != null)
                {
                    return gameObject;
                }
                else
                {
                    gameObject = ExtendList(name, list, dictionary, parent);
                    return gameObject;
                }
            }

            return gameObject;
        }

        private GameObject ExtendList(string name, List<GameObject> list, Dictionary<string, List<GameObject>> dictionary, Transform parent)
        {
            List<GameObject> newList = new List<GameObject>(list.Count + AdditionalCount);
            newList.AddRange(list);

            for (int i = 0; i < AdditionalCount; i++)
            {
                GameObject newGameObject = Object.Instantiate((newList[0]), parent);
                newGameObject.SetActive(false);
                newList.Add(newGameObject);
            }

            dictionary[name] = newList;
            dictionary.TryGetValue(name, out List<GameObject> list1);
            GameObject gameObject = list1?.FirstOrDefault(it => it.activeInHierarchy == false);

            if (gameObject != null)
                return gameObject;
            else
                return ExtendList(name, list1, dictionary, parent);
        }
    }
}