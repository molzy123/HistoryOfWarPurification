using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RangeTrigger : MonoBehaviour
{
    
    public delegate void RangeTriggerDelegate(Collider2D other);
    public RangeTriggerDelegate onEnterRange, onExitRange;
    
    // 管理触发器范围内的对象
    private List<Collider2D> _colliders = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_colliders.Contains(other))
        {
            _colliders.Add(other);
        }
        onEnterRange(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_colliders.Contains(other))
        {
            _colliders.Remove(other);
        }
        onExitRange(other);
    }
}
