using SpawnManagers;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _distanceToAttack;

        public float DistanceToPlayer { get; private set; }

        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int Throw = Animator.StringToHash("Throw");
        private const string BulletTag = "Bullet";
    
        private Vector3 _player;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void FollowPlayer(Vector3 player)
        {
            var distance = Vector3.Distance(transform.position, player);
            if (distance < _distanceToAttack && DistanceToPlayer > _distanceToAttack)
                _animator.SetTrigger(Throw);
            if (distance > _distanceToAttack && DistanceToPlayer < _distanceToAttack)
                _animator.SetTrigger(Running);
            _player = player;
            DistanceToPlayer = distance;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _player, Time.deltaTime * _speed);
            transform.LookAt(_player);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(BulletTag))
            {
                other.gameObject.SetActive(false);
                GetComponentInParent<EnemiesManager>().EnemyDeath(transform);
            }
        }
    }
}
