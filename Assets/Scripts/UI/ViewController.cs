using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ViewController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerMovementController _playerMovement;
        [SerializeField] private PlayerColliderController _playerCollider;
        [Header("UI")]
        [SerializeField] private FloatingJoystick _floatingJoystick;
        [SerializeField] private TextMeshProUGUI _moneyFieldText;
        [SerializeField] private TextMeshProUGUI _gemFieldText;
        [SerializeField] private TextMeshProUGUI _moneyBaseText;
        [SerializeField] private TextMeshProUGUI _gemBaseText;
        [SerializeField] private Slider _health;
        [SerializeField] private float _hpChange;
        [SerializeField] private GameObject _endingScreen;
        [SerializeField] private TextMeshProUGUI _moneyFinalText;
        [SerializeField] private TextMeshProUGUI _gemFinalText;

        private int _moneyField;
        private int _gemField;
        private int _moneyBase;
        private int _gemBase;

        private void OnEnable()
        {
            _floatingJoystick.OnDragging += _playerMovement.Movement;
            _playerCollider.OnMoneyCollected += CoinCollected;
            _playerCollider.OnGemCollected += GemCollected;
            _playerCollider.OnHit += Hit;
            _playerCollider.OnBaseEntered += BaseEntered;
        }

        private void CoinCollected()
        {
            _moneyField++;
            _moneyFieldText.text = _moneyField.ToString();
        }

        private void GemCollected()
        {
            _gemField++;
            _gemFieldText.text = _gemField.ToString();
        }

        private void BaseEntered()
        {
            _moneyBase += _moneyField;
            _moneyBaseText.text = _moneyBase.ToString();
            _moneyField = 0;
            _moneyFieldText.text = _moneyField.ToString();
        
            _gemBase += _gemField;
            _gemBaseText.text = _gemBase.ToString();
            _gemField = 0;
            _gemFieldText.text = _gemField.ToString();
        }

        private void Hit()
        {
            _health.value -= _hpChange;
            if (_health.value == 0)
                Death();
        }

        public void Death()
        {
            _moneyFinalText.text = _moneyBase.ToString();
            _gemFinalText.text = _gemBase.ToString();
            _endingScreen.SetActive(true);
            Time.timeScale = 0;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }

        private void OnDisable()
        {
            _floatingJoystick.OnDragging -= _playerMovement.Movement;
            _playerCollider.OnMoneyCollected -= CoinCollected;
            _playerCollider.OnGemCollected -= GemCollected;
            _playerCollider.OnHit -= Hit;
            _playerCollider.OnBaseEntered -= BaseEntered;
        }
    }
}
