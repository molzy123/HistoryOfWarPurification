using System;
using System.Collections.Generic;
using affiliation;
using character.solider;
using common.stateMachine;
using DefaultNamespace;
using ui.frame;
using Unity.VisualScripting;
using IState = common.stateMachine.IState;

namespace game_core
{
    public class GameLoginState: IState
    {
        
        private StateMachine<GameStateEnum> _machine;
        
        public GameLoginState(StateMachine<GameStateEnum> machine)
        {
            _machine = machine;
        }
        
        public void enter()
        {
            Locator.fetch<ViewManager>().showView("LoginView");
        }

        public void update()
        {

        }

        public void exit()
        {
            Locator.fetch<ViewManager>().hideView("LoginView");
        }
    }
}