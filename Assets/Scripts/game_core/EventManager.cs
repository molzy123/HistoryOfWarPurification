using System;
using System.Collections.Generic;

namespace game_core
{
    public static class EventManager
    {
        // 字典：事件名 => 委托列表
        private static Dictionary<EventEnum, Action<object>> eventTable = new Dictionary<EventEnum, Action<object>>();

        // 注册监听
        public static void AddListener(EventEnum eventName, Action<object> listener)
        {
            if (!eventTable.ContainsKey(eventName))
                eventTable[eventName] = delegate { };

            eventTable[eventName] += listener;
        }

        // 移除监听
        public static void RemoveListener(EventEnum eventName, Action<object> listener)
        {
            if (eventTable.ContainsKey(eventName))
                eventTable[eventName] -= listener;
        }

        // 事件分发
        public static void Dispatch(EventEnum eventName, object param = null)
        {
            if (eventTable.ContainsKey(eventName))
                eventTable[eventName]?.Invoke(param);
        }
    }
}