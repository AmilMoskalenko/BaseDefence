using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpawnManagers
{
    public class LootManager : MonoBehaviour
    {
        [Header("Money")]
        [SerializeField] private GameObject _money;
        [SerializeField] private int _amountPoolMoney;
        [Header("Gem")]
        [SerializeField] private GameObject _gem;
        [SerializeField] private int _amountPoolGem;
        [Header("Probability percent")]
        [SerializeField] private int _moneyProbability;

        private List<GameObject> _pooledObjectsMoney;
        private List<GameObject> _pooledObjectsGem;

        private void Start()
        {
            _pooledObjectsMoney = PoolCreating(_amountPoolMoney, _money);
            _pooledObjectsGem = PoolCreating(_amountPoolGem, _gem);
        }

        private List<GameObject> PoolCreating(int amount, GameObject loot)
        {
            var pooledObjects = new List<GameObject>();
            for (int i = 0; i < amount; i++)
            {
                var tmp = Instantiate(loot, transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
            return pooledObjects;
        }
    
        public GameObject GetPooledLoot()
        {
            var random = Random.Range(0, 101);
            return random <= _moneyProbability ? GetPooledObject(_pooledObjectsMoney) : GetPooledObject(_pooledObjectsGem);
        }
    
        private GameObject GetPooledObject(List<GameObject> pooledObjects) => pooledObjects.FirstOrDefault(t => !t.activeInHierarchy);
    }
}
