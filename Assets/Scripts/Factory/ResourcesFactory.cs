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
            GameObject prefab = loadPrefab(prefabName);
            if (prefab == null)
            {
                Debug.LogError(prefabName + " could not be loaded");
                return null;
            }
            return Object.Instantiate(prefab, parent.transform);
        }


        public GameObject loadGameObject(string prefabName)
        {
            GameObject prefab = loadPrefab(prefabName);
            if (prefab == null)
            {
                Debug.LogError(prefabName + " could not be loaded");
                return null;
            }
            return Object.Instantiate(prefab);
        }



    }
}