using System.Collections.Generic;
using DefaultNamespace;

namespace GameCore
{
    public class UpdateService: AbstractModule
    {
        
        private HashSet<IUpdate> _handlers = new HashSet<IUpdate>();
        
        public void addHandler(IUpdate handler)
        {
            _handlers.Add(handler);
        }

        public void removeHandler(IUpdate handler)
        {
            _handlers.Remove(handler);
        }

        public override void update()
        {
            foreach (IUpdate handler in _handlers)
            {
                handler.update();
            }
        }
    }
}