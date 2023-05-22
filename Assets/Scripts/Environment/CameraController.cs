using UnityEngine;

namespace Environment
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offset;

        private void Update()
        {
            transform.position = _player.position + _offset;
        }
    }
}
