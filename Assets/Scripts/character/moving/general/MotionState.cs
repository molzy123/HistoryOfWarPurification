using character.solider;
using Unity.VisualScripting;
using IState = common.stateMachine.IState;

namespace character.moving
{
    public abstract class MotionState : IState
    {

        public MotionStatusEnum type;
        
        public virtual void enter()
        {
            
        }

        public virtual void update()
        {
            
        }

        public virtual void exit()
        {
            
        }
    }
}