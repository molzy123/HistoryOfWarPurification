using System;

namespace game_core
{
    public class RequestIdGenerator
    {
        private static int sequence = 0; // 自增序列
        private static readonly object lockObj = new object(); // 锁对象，保证线程安全
        
        
        public static string Generate()
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(); // 获取当前时间戳（毫秒）
            int seq;

            // 确保序列号线程安全
            lock (lockObj)
            {
                seq = sequence++;
                if (sequence > 9999) // 防止序列号无限增长
                {
                    sequence = 0;
                }
            }

            // 生成唯一 ID，例如：REQ-<时间戳>-<序列号>
            return $"{timestamp}-{seq:D4}";
        }
    }
}