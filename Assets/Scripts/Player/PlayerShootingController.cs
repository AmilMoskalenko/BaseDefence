using System.Linq;
using SpawnManagers;
using UnityEngine;

namespace Player
{
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField] private float _speedBullet;
        [SerializeField] private float _delayShooting;
        [SerializeField] private BulletsManager _bulletsManager;
        [SerializeField] private EnemiesManager _enemiesManager;

        private PlayerMovementController _player;
        private float _timer;

        private void Start()
        {
            _player = GetComponent<PlayerMovementController>();
        }

        private void Update()
        { 
            if (_player.InField)
                Shooting();
        }
    
        private void Shooting()
        {
            _timer += Time.deltaTime;
            var distances = _enemiesManager.Enemies.Select(enemy => enemy.DistanceToPlayer).ToList();
            var min = distances.Min();
            var index = distances.IndexOf(min);
            var target = _enemiesManager.Enemies[index].transform.position;
            transform.LookAt(target);
            if (_timer >= _delayShooting)
            {
                _timer = 0f;
                var bullet = _bulletsManager.GetPooledObject();
                bullet.transform.position = transform.position + Vector3.up;
                bullet.SetActive(true);
                var rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.AddForce(transform.forward * _speedBullet, ForceMode.Impulse);
            }
        }
    }
}
