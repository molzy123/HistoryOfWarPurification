using character.solider;
using UnityEngine;
using IState = common.stateMachine.IState;

namespace character.moving
{
    public class MoveState : MotionState
    {
        private readonly Animator _animator;
        private readonly MovingController _movingController;
        
        public MoveState(Animator animator, MovingController movingController)
        {
            type = MotionStatusEnum.MOVING;
            _animator = animator;
            _movingController = movingController;
        }

        public override void enter()
        {
            _animator.enabled = true;
            _animator.Play("Run");
        }

        public override void update()
        {
            _movingController.update();
        }

        public override void exit()
        {
            _animator.enabled = false;
        }
    }
}