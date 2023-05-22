using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public event Action<Vector3> OnMoving = delegate { };

        public bool InField { get; set; }

        private PlayerAnimController _animController;
        private Vector2 _inputSaved;

        private void Start()
        {
            _animController = GetComponent<PlayerAnimController>();
            StartCoroutine(PlayerPosition());
        }

        private IEnumerator PlayerPosition()
        {
            while (gameObject.activeSelf)
            {
                OnMoving?.Invoke(transform.position);
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void Movement(Vector2 input)
        {
            if (_inputSaved == Vector2.zero && input != Vector2.zero && !InField)
                _animController.RunningAnim();
            if (_inputSaved != Vector2.zero && input == Vector2.zero && !InField)
                _animController.IdleAnim();
            _inputSaved = input;
            if (input != Vector2.zero && !InField)
            {
                var angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            }
        }

        private void Update()
        {
            if (_inputSaved == Vector2.zero)
                return;
            transform.position += new Vector3(_inputSaved.x * _speed * Time.deltaTime, 0, _inputSaved.y * _speed * Time.deltaTime);
        }
    }
}
