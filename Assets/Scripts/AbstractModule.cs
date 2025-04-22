using System;
using game_core;
using GameCore;
using UnityEngine;

namespace DefaultNamespace
{
    public class AbstractModule : IModule
    {
        protected AbstractModule()
        {
            // 注册子类的类型到Locator中
            Locator.register(GetType(), this);
        }

        public virtual void initialize()
        {
            Debug.Log(GetType().Name + " initialized");
        }

        public virtual void start()
        {
            Debug.Log(GetType().Name + " started");
        }

        public virtual void destroy()
        {
            Debug.Log(GetType().Name + " destroyed");
        }

        public virtual void update()
        {
            
        }
    }
}