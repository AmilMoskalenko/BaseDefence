using UnityEngine;

namespace Environment
{
    public class WallController : MonoBehaviour
    {
        private const string BulletTag = "Bullet";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(BulletTag))
                other.gameObject.SetActive(false);
        }
    }
}
