using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ui.frame
{
    public abstract class ViewBase : UIBase, IView
    {
        // 关闭之后缓存15秒
        private static readonly int _cacheSeconds = 15;
        private readonly HashSet<Button> _buttonClickListeners = new HashSet<Button>();
        
        private Coroutine _cacheCoroutine = null;
        
        protected virtual void onShow()
        {
            if(_cacheCoroutine != null) 
            {
                StopCoroutine(_cacheCoroutine);
                _cacheCoroutine = null;
            }
        }

        protected virtual void onHide()
        {
            if (_cacheCoroutine != null)
            {
                StopCoroutine(_cacheCoroutine);
            }
            _cacheCoroutine = StartCoroutine(CacheTimerCoroutine(_cacheSeconds));
        }
        
        // 协程实现的缓存定时器
        private IEnumerator CacheTimerCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            OnTimerCompleted();
            _cacheCoroutine = null;
        }

        // 定时器完成后执行的方法
        protected virtual void OnTimerCompleted()
        {
            Destroy(gameObject);
        }

        public virtual void initialize()
        {
            
        }

        public void show()
        {
            gameObject.SetActive(true);
            onShow();
        }

        public void hide()
        {
            foreach (Button button in _buttonClickListeners)
            {
                button.onClick.RemoveAllListeners();
            }
            onHide();
            gameObject.SetActive(false);
        }
        
        protected void onClick(Button button, UnityAction call)
        {
            if (button == null || call == null) return;
            button.onClick.AddListener(call);
            _buttonClickListeners.Add(button);
        }

    }
}