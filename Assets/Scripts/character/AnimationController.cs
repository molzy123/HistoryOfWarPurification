using UnityEngine;

namespace character
{
    public class AnimationController : IAnimationController
    {
        private Animator _animator;
        
        public AnimationController(Animator animator)
        {
            _animator = animator;
        }
        
        public void play(string animationName)
        {
            _animator.enabled = false;
            _animator.Play(animationName);
        }

        public void stop()
        {
            _animator.enabled = false;
        }

        public void isPlaying()
        {
            
        }
    }
}