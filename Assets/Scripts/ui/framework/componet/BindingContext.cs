using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UI;
using ui.frame;
using UnityEngine;
using Object = UnityEngine.Object;

public class BindingContext : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> bindings;

    void Awake()
    {
        _bindingAttribute();
    }
    
    private void _bindingAttribute()
    {
        UIBase uiBase = GetComponent<UIBase>();
        // 获取所有属性
        var propertyInfos = uiBase.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(p=> p.GetCustomAttribute(typeof(BindingAttribute)) != null);
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
            propertyInfo.SetValue(uiBase, component);
        }
    }
}
