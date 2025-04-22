using DefaultNamespace;
using UnityEngine;

namespace game_core
{
    public class GameObjectUIService: AbstractModule
    {
        private readonly GameObject _followUIRoot;
        
        
        public GameObjectUIService(GameObject followUIRoot)
        {
            _followUIRoot = followUIRoot;
        }
        
        
        public GameObject loadGameObjectUI(string prefabName)
        {
            GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/" + prefabName), _followUIRoot.transform,false);
            return gameObject;
        }
        
    }
}