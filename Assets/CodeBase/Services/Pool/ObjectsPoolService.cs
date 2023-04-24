using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Constructor;
using CodeBase.StaticData.Hits;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.ShotVfxs;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Elements.ShopPanel;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public class ObjectsPoolService : IObjectsPoolService
    {
        private const int InitialCapacity = 5;
        private const int AdditionalCount = 5;

        private IAssets _assets;
        private IConstructorService _constructorService;
        private Dictionary<string, List<GameObject>> _heroProjectiles;
        private Dictionary<string, List<GameObject>> _enemyProjectiles;
        private Dictionary<string, List<GameObject>> _shotVfxs;
        private Dictionary<string, List<GameObject>> _shopItems;
        private Transform _enemyProjectilesRoot;
        private Transform _heroProjectilesRoot;
        private Transform _shotVfxsRoot;
        private Transform _shopItemsRoot;

        public ObjectsPoolService(IAssets assets, IConstructorService constructorService)
        {
            _assets = assets;
            _constructorService = constructorService;
        }

        public void GenerateObjects()
        {
            CreateRoots();
        }

        private async void CreateRoots()
        {
            GameObject root = await _assets.Load<GameObject>(AssetAddresses.EnemyProjectilesRoot);
            GameObject gameObject = Object.Instantiate(root);
            _enemyProjectilesRoot = gameObject.transform;

            root = await _assets.Load<GameObject>(AssetAddresses.HeroProjectilesRoot);
            gameObject = Object.Instantiate(root);
            _heroProjectilesRoot = gameObject.transform;

            root = await _assets.Load<GameObject>(AssetAddresses.ShotVfxsRoot);
            gameObject = Object.Instantiate(root);
            _shotVfxsRoot = gameObject.transform;

            root = await _assets.Load<GameObject>(AssetAddresses.ShopItemsRoot);
            gameObject = Object.Instantiate(root);
            _shopItemsRoot = gameObject.transform;

            GenerateEnemyProjectiles();
            GenerateHeroProjectiles();
            GenerateShotVfxs();
            GenerateShopItems();
        }

        private async void GenerateEnemyProjectiles()
        {
            _enemyProjectiles = new Dictionary<string, List<GameObject>>();
            List<GameObject> gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject projectile = await _assets.Instantiate(AssetAddresses.PistolBullet, _enemyProjectilesRoot);
                _constructorService.ConstructEnemyProjectile(projectile, ProjectileTypeId.PistolBullet);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _enemyProjectiles.Add(EnemyWeaponTypeId.Pistol.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject projectile = await _assets.Instantiate(AssetAddresses.Shot, _enemyProjectilesRoot);
                _constructorService.ConstructEnemyProjectile(projectile, ProjectileTypeId.Shot);
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
                GameObject projectile = await _assets.Instantiate(AssetAddresses.Grenade, _heroProjectilesRoot);
                _constructorService.ConstructHeroProjectile(projectile, ProjectileTypeId.Grenade, BlastTypeId.Grenade,
                    HeroWeaponTypeId.GrenadeLauncher);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _heroProjectiles.Add(HeroWeaponTypeId.GrenadeLauncher.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject projectile = await _assets.Instantiate(AssetAddresses.RpgRocket, _heroProjectilesRoot);
                _constructorService.ConstructHeroProjectile(projectile, ProjectileTypeId.RpgRocket,
                    BlastTypeId.RpgRocket, HeroWeaponTypeId.RPG);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _heroProjectiles.Add(HeroWeaponTypeId.RPG.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject projectile =
                    await _assets.Instantiate(AssetAddresses.RocketLauncherRocket, _heroProjectilesRoot);
                _constructorService.ConstructHeroProjectile(projectile, ProjectileTypeId.RocketLauncherRocket,
                    BlastTypeId.RocketLauncherRocket, HeroWeaponTypeId.RocketLauncher);
                projectile.SetActive(false);
                gameObjects.Add(projectile);
            }

            _heroProjectiles.Add(HeroWeaponTypeId.RocketLauncher.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject projectile = await _assets.Instantiate(AssetAddresses.Bomb, _heroProjectilesRoot);
                _constructorService.ConstructHeroProjectile(projectile, ProjectileTypeId.Bomb, BlastTypeId.Bomb,
                    HeroWeaponTypeId.Mortar);
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

        public async void GenerateShopItems()
        {
            _shopItems = new Dictionary<string, List<GameObject>>();
            List<GameObject> gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject item = await _assets.Instantiate(AssetAddresses.ShopItem, _shopItemsRoot);
                item.SetActive(false);
                gameObjects.Add(item);
            }

            _shopItems.Add(ShopItemTypeIds.Item.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject item = await _assets.Instantiate(AssetAddresses.ShopAmmo, _shopItemsRoot);
                item.SetActive(false);
                gameObjects.Add(item);
            }

            _shopItems.Add(ShopItemTypeIds.Ammo.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject item = await _assets.Instantiate(AssetAddresses.ShopUpgrade, _shopItemsRoot);
                item.SetActive(false);
                gameObjects.Add(item);
            }

            _shopItems.Add(ShopItemTypeIds.Upgrade.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject item = await _assets.Instantiate(AssetAddresses.ShopPerk, _shopItemsRoot);
                item.SetActive(false);
                gameObjects.Add(item);
            }

            _shopItems.Add(ShopItemTypeIds.Perk.ToString(), gameObjects);
            gameObjects = new List<GameObject>(InitialCapacity);

            for (int i = 0; i < gameObjects.Capacity; i++)
            {
                GameObject item = await _assets.Instantiate(AssetAddresses.ShopWeapon, _shopItemsRoot);
                item.SetActive(false);
                gameObjects.Add(item);
            }

            _shopItems.Add(ShopItemTypeIds.Weapon.ToString(), gameObjects);
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

        public GameObject GetShopItem(string name) =>
            GetGameObject(name, _shopItems, _shopItemsRoot);

        public void ReturnEnemyProjectile(GameObject gameObject) =>
            ReturnGameObject(gameObject, _enemyProjectilesRoot);

        public void ReturnHeroProjectile(GameObject gameObject) =>
            ReturnGameObject(gameObject, _heroProjectilesRoot);

        public void ReturnShotVfx(GameObject gameObject) =>
            ReturnGameObject(gameObject, _shotVfxsRoot);

        public void ReturnShopItem(GameObject gameObject) =>
            ReturnGameObject(gameObject, _shopItemsRoot);

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

        private GameObject ExtendList(string name, List<GameObject> list,
            Dictionary<string, List<GameObject>> dictionary, Transform parent)
        {
            List<GameObject> newList = new List<GameObject>(list.Count + AdditionalCount);
            newList.AddRange(list);

            for (int i = 0; i < AdditionalCount; i++)
            {
                GameObject original = newList[0];
                GameObject newGameObject = Object.Instantiate(original, parent);
                _constructorService.ConstructProjectileLike(original, newGameObject);
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