using System;
using System.Collections.Generic;
using DefaultNamespace;

namespace game_core
{
    public class EventService : AbstractModule
    {
        
        private Dictionary<EventEnum, Delegate> _handlers = new Dictionary<EventEnum, Delegate>();
        
        
        
    }
}