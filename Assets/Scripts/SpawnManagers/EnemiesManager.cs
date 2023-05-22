using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Player;
using UnityEngine;

namespace SpawnManagers
{
    public class EnemiesManager : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] private int _amountPool;
        [SerializeField] private float _delaySpawn;
        [SerializeField] private Vector2 _spawnRangeX;
        [SerializeField] private Vector2 _spawnRangeZ;
        [SerializeField] private PlayerMovementController _player;
        [SerializeField] private LootManager _lootManager;

        public List<EnemyController> Enemies { get; private set; }

        private List<GameObject> _pooledObjects;

        private void Start()
        {
            _pooledObjects = new List<GameObject>();
            for (int i = 0; i < _amountPool; i++)
            {
                var tmp = Instantiate(_enemy, transform);
                tmp.SetActive(false);
                _pooledObjects.Add(tmp);
            }
        
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            Enemies = new List<EnemyController>();
            while (GetPooledObject() != null)
            {
                var enemy = GetPooledObject();
                enemy.transform.position = new Vector3(Random.Range(_spawnRangeX.x, _spawnRangeX.y), 0, Random.Range(_spawnRangeZ.x, _spawnRangeZ.y));
                enemy.SetActive(true);
                var enemyController = enemy.GetComponent<EnemyController>();
                _player.OnMoving += enemyController.FollowPlayer;
                Enemies.Add(enemyController);
                yield return new WaitForSeconds(_delaySpawn);
            }
        }
    
        private GameObject GetPooledObject() => _pooledObjects.FirstOrDefault(t => !t.activeInHierarchy);
        
        public void EnemyDeath(Transform enemy)
        {
            var loot = _lootManager.GetPooledLoot();
            loot.transform.position = enemy.position + Vector3.up;
            loot.transform.rotation = Random.rotation;
            loot.SetActive(true);
            enemy.position = new Vector3(Random.Range(_spawnRangeX.x, _spawnRangeX.y), 0, Random.Range(_spawnRangeZ.x, _spawnRangeZ.y));
        }
    }
}
