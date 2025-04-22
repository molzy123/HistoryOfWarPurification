using System;
using System.Collections.Generic;
using DefaultNamespace;

namespace common.stateMachine

{
    public class StateMachine<TStateEnum>
    {
        private IState _currentState;
        public TStateEnum currentStateType;
        
        private readonly Dictionary<TStateEnum, IState> _stateMap = new Dictionary<TStateEnum, IState>();
        
        public void addState(TStateEnum type, IState state)
        {
            if (_stateMap.ContainsKey(type))
            {
                return;
            }
            _stateMap.Add(type, state);
        }
        
        public void delState(TStateEnum stateEnum)
        {
            _stateMap.Remove(stateEnum);
        }

        public void switchState(TStateEnum type)
        {
            if (!_stateMap.ContainsKey(type))
            {
                return;
            }
            if (_currentState != null)
            {
                _currentState.exit();
            }
            

            _currentState = _stateMap[type];
            _currentState.enter();
            currentStateType = type;
        }

        public void update()
        {
            if (_currentState != null)
            {
                _currentState.update();
            }
        }
    }
}