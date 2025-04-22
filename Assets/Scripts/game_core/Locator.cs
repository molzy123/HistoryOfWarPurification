using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Locator
    {
        
        private static Dictionary<Type,Object> _instances = new Dictionary<Type, Object>();
        
        public static void register<T>(Object instance)
        {
            _instances[typeof(T)] = instance;
        }
        
        public static void register(Type type, Object instance)
        {
            _instances[type] = instance;
        }
        
        public static void unregister<T>()
        {
            _instances.Remove(typeof(T));
        }
        
        public static T fetch<T>()
        {
            return (T) _instances[typeof(T)];
        }
        
    }
}