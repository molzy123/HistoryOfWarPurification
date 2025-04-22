using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using game_core;
using ui.frame;
using UnityEngine;
using Object = System.Object;


[AttributeUsage(AttributeTargets.Property)]
public class BindingGo : System.Attribute
{
    public BindingGo(string description)
    {
        this.description = description;
    }

    public string description { get; }
}

namespace UI
{
    public abstract class UIBase : GameObjectSolid
    {
        protected Object bindingGo;
        
        
        
        private void _bindingContext()
        {
            // 获取BindingContext组件
            BindingContext bindingContext = gameObject.GetComponent<BindingContext>();
            PropertyInfo propertyInfo = GetType().GetProperty("bindingGo");
            if (propertyInfo == null)
            {
                Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>" + GetType().Name + "没有bindingGo属性");
                return;
            }
            // 获取属性的类型
            Type type = propertyInfo.GetType();
            // 根据类型创建实例
            Object value = Activator.CreateInstance(type);
            // 遍历所有绑定的物体
            foreach (GameObject contextBinding in bindingContext.bindings)
            {
                // 根据物体的名字获取属性名
                string propertyName = contextBinding.gameObject.name.Substring(1);
                // 获取属性信息
                PropertyInfo goPropertyInfo = type.GetProperty(propertyName);
                if (goPropertyInfo != null)
                {
                    // 给属性赋值
                    goPropertyInfo.SetValue(value, contextBinding.GetComponent(goPropertyInfo.GetType()));
                }
            }
            // 给属性赋值
            propertyInfo.SetValue(this, value);
        }
    }
}