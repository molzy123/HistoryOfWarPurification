using System;
using building;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.map
{
    public class GridCell: MonoBehaviour
    {
        
        public bool isSpace = true;
        
        public static Color defaultColor = Color.gray;
        public static Color successColor = Color.green;
        public static Color warningColor = Color.red;

        public void setPosition(int x, int z)
        {
            transform.position = new Vector3(x,0,z);
        }
        
        public void build(GameObject buildGo)
        {
            // 放置完成后，恢复碰撞器
            Collider cubeCollider = buildGo.GetComponent<Collider>();
            if (cubeCollider != null)
            {
                cubeCollider.enabled = true;
            }
            buildGo.transform.position = transform.position;
            buildGo.transform.parent = transform;
            buildGo.SetActive(true);
            isSpace = false;
        }

        public bool canBuild(Building building)
        {
            if (!isSpace) return false;
            return true;
        }

        public void updateState(bool building)
        {
            
            // 修改材质的颜色
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                if (!building)
                {
                    renderer.material.color = defaultColor;
                    return;
                }
                renderer.material.color = isSpace ? successColor : warningColor;
            }
        }
    }
}