using System;
using UnityEngine;

namespace Player
{
    public class PlayerColliderController : MonoBehaviour
    {
        public event Action OnMoneyCollected = delegate { };
        public event Action OnGemCollected = delegate { };
        public event Action OnHit = delegate { };
        public event Action OnBaseEntered = delegate { };
    
        private const string MoneyTag = "Money";
        private const string GemTag = "Gem";
        private const string EnemyTag = "Enemy";
        private const string BaseTag = "Base";
        private const string FieldTag = "Field";
        
        private PlayerMovementController _player;
        private PlayerAnimController _animController;
        
        private void Start()
        {
            _player = GetComponent<PlayerMovementController>();
            _animController = GetComponent<PlayerAnimController>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(MoneyTag))
            {
                OnMoneyCollected?.Invoke();
                other.gameObject.SetActive(false);
            }
            if (other.collider.CompareTag(GemTag))
            {
                OnGemCollected?.Invoke();
                other.gameObject.SetActive(false);
            }
            if (other.collider.CompareTag(EnemyTag))
            {
                OnHit?.Invoke();
            }
            if (other.collider.CompareTag(BaseTag) && _player.InField)
            {
                _player.InField = false;
                _animController.RunningAnim();
                OnBaseEntered?.Invoke();
            }
            if (other.collider.CompareTag(FieldTag) && !_player.InField)
            {
                _player.InField = true;
                _animController.ShootingAnim();
            }
        }
    }
}
