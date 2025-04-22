using System;
using character.solider;
using common.stateMachine;
using UnityEngine;

namespace character.moving
{
    public class IdleState: MotionState
    {
        
        private Animator _animator;
        public IdleState(Animator animator)
        {
            _animator = animator;
            type = MotionStatusEnum.IDLE;
        }
        
        public override void enter()
        {
            _animator.enabled = true;
            _animator.Play("Idle");
        }

        public override void exit()
        {
            _animator.enabled = false;
        }

        
    }
}