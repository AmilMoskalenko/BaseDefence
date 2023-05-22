using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpawnManagers
{
    public class BulletsManager : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private int _amountPool;
    
        private List<GameObject> _pooledObjects;

        private void Start()
        {
            _pooledObjects = new List<GameObject>();
            for (int i = 0; i < _amountPool; i++)
            {
                var tmp = Instantiate(_bullet, transform);
                tmp.SetActive(false);
                _pooledObjects.Add(tmp);
            }
        }
    
        public GameObject GetPooledObject() => _pooledObjects.FirstOrDefault(t => !t.activeInHierarchy);
    }
}
