using System;

namespace common.stateMachine
{
    public interface IState
    {
        void enter();
        void update();
        void exit();
    }
}