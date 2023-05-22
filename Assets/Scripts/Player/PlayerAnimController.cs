using UnityEngine;

namespace Player
{
    public class PlayerAnimController : MonoBehaviour
    {
        private static readonly int DynIdle = Animator.StringToHash("DynIdle");
        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int RiffleWalk = Animator.StringToHash("RiffleWalk");
    
        private Animator _animator;
    
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void RunningAnim()
        {
            _animator.SetTrigger(Running);
        }

        public void IdleAnim()
        {
            _animator.SetTrigger(DynIdle);
        }

        public void ShootingAnim()
        {
            _animator.SetTrigger(RiffleWalk);
        }
    }
}
