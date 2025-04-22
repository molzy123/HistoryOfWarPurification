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
    public class GameStartState: IState
    {

        private List<IModule> _modules;
        private StateMachine<GameStateEnum> _machine;
        
        public GameStartState(StateMachine<GameStateEnum> machine)
        {
            _machine = machine;
            _modules = new List<IModule>();
            _modules.Add(new AffiliationManager());
            _modules.Add(new SoliderManager());
        }
        
        public void enter()
        {
            foreach (IModule module in _modules) { module.initialize(); }
            foreach (IModule module in _modules) { module.start(); }
            
            Locator.fetch<ViewManager>().showView("HudViewUI");
            Locator.fetch<ViewManager>().showView("WebSocketTestView");
            
        }

        public void update()
        {
            foreach (IModule module in _modules) { module.update(); }
        }

        public void exit()
        {
            foreach (IModule module in _modules) { module.destroy(); }
            _modules.Clear();
        }
    }
}