using System.Collections.Generic;
using UnityEngine;

namespace Editor
{
    public class EditorUtil
    {
        public static void GetAllChildren(Transform parent, List<GameObject> resultList)
        {
            foreach (Transform child in parent)
            {
                resultList.Add(child.gameObject);
                GetAllChildren(child, resultList);
            }
        }
    }
}