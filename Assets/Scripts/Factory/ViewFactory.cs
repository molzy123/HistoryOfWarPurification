using System;
using Factory;
using UI;
using ui.frame;
using UnityEngine;
using Object = UnityEngine.Object;

namespace game_core
{
    public class ViewFactory
    {
        private ResourcesFactory _resourcesFactory;
        
        public ViewFactory(ResourcesFactory resourcesFactory)
        {
            _resourcesFactory = resourcesFactory;
        }
        
        public IView loadView(string prefabName, GameObject parentGo)
        {
            GameObject viewGameObject = _resourcesFactory.loadGameObject(prefabName, parentGo);
            IView view = viewGameObject.GetComponent<IView>();
            view.initialize();
            return view;
        }

    }
}