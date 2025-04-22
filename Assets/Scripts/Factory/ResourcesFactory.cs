using character;
using DefaultNamespace;
using UnityEngine;

namespace Factory
{
    public class ResourcesFactory
    {
        public GameObject loadPrefab(string prefabName)
        {
            return Resources.Load<GameObject>("Prefabs/" + prefabName);
        }
        
        public GameObject loadGameObject(string prefabName, GameObject parent)
        {
            return Object.Instantiate(loadPrefab(prefabName), parent.transform);
        }


        public GameObject loadGameObject(string prefabName)
        {
            return Object.Instantiate(loadPrefab(prefabName));
        }



    }
}