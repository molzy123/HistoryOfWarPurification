using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using game_core;
using ui.frame;
using UnityEngine;

namespace UI
{
    public abstract class GameObjectSolid : MonoBehaviour
    {
        public virtual void initialize()
        {
            
        }
        
        private void _bindingAttribute()
        {
            // 获取所有属性
            var propertyInfos = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(p=> p.GetCustomAttribute(typeof(BindingAttribute)) != null);
            List<GameObject> bindings = gameObject.GetComponent<BindingContext>().bindings;
            foreach (var propertyInfo in propertyInfos)
            {
                BindingAttribute annotation = propertyInfo.GetCustomAttribute<BindingAttribute>();
                string uiName = annotation.uiName ?? propertyInfo.Name;

                GameObject obj = bindings.Find(go => go.name == uiName);
                if (obj == null)
                {
                    Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>" + GetType().Name + "没有找到" + uiName + "物体");
                    continue;
                }
                
                Component component = obj.GetComponent(propertyInfo.PropertyType);
                if (component == null)
                {
                    Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>" + GetType().Name + "没有找到" + uiName + "物体的" + propertyInfo.PropertyType.Name + "组件");
                    continue;
                }
                propertyInfo.SetValue(this, component);
            }
        }
    }
}