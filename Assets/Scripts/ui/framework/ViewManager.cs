using System.Collections.Generic;
using DefaultNamespace;
using Factory;
using game_core;
using ui.frame;
using ui.framework;
using UnityEngine;

namespace ui.frame
{
    public class ViewManager : AbstractModule
    {
        public IView currentView;
        
        private readonly ViewFactory _viewFactory;

        private readonly GameObject _viewRoot;
        
        private readonly Dictionary<UILayer, Transform> _layerMap = new Dictionary<UILayer, Transform>();
        
        private readonly Dictionary<string, IView> _viewMap = new Dictionary<string, IView>();

        public ViewManager(GameObject viewRoot)
        {
            _viewRoot = viewRoot;
            _layerMap.Add(UILayer.NORMAL, _viewRoot.transform.Find("Normal"));
            _layerMap.Add(UILayer.TIPS, _viewRoot.transform.Find("Tips"));
            _layerMap.Add(UILayer.POPUP, _viewRoot.transform.Find("Popup"));
            _layerMap.Add(UILayer.FTUE, _viewRoot.transform.Find("FTUE"));
            
            _viewFactory = new ViewFactory(new ResourcesFactory());
        }

        public override void initialize()
        {
        }

        public IView showView(string viewName, UILayer layer = UILayer.NORMAL)
        {
            Transform parent = _layerMap[layer];
            IView view = _viewFactory.loadView(viewName, parent.gameObject);
            _viewMap.Add(viewName, view);
            currentView = view;
            currentView.show();
            return view;
        }

        public void hideView(string viewName)
        {
            _viewMap.TryGetValue(viewName, out IView view);
            if (view != null)
            {
                view.hide();
                _viewMap.Remove(viewName);
            }
        }

        public override void update()
        {
        }
    }
}