using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData.Monster;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] public MonsterTypeId MonsterTypeId;
        public string Id { get; set; }

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory factory) =>
            _factory = factory;

        public void Initialize()
        {
            Spawn();
        }

        private async void Spawn()
        {
            GameObject monster = await _factory.CreateMonster(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Died += Slain;
        }

        private void Slain()
        {
            if (_enemyDeath != null)
                _enemyDeath.Died -= Slain;
        }
    }
}