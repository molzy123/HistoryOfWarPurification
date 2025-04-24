using System;
using System.Collections.Generic;
using affiliation;
using building;
using character.solider;
using common.stateMachine;
using DefaultNamespace;
using DefaultNamespace.map;
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
            // _modules.Add(new AffiliationManager());
            // _modules.Add(new SoliderManager());
            _modules.Add(new GridMapManager());
            _modules.Add(new BuildingManager());
            
        }
        
        public void enter()
        {
            foreach (IModule module in _modules) { module.initialize(); }
            foreach (IModule module in _modules) { module.start(); }
            
            Locator.fetch<ViewManager>().showView("HudView");
        }

        public void update()
        {
            foreach (IModule module in _modules) { module.update(); }
        }

        public void exit()
        {
            Locator.fetch<ViewManager>().hideView("HudView");
            foreach (IModule module in _modules) { module.OnDestroy(); }
            _modules.Clear();
            
        }
    }
}