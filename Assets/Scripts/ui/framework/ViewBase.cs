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
        
        private Timer _cacheTimer = null;

        protected virtual void onShow()
        {
            if(_cacheTimer != null) _cacheTimer.Close();
        }

        protected virtual void onHide()
        {
            if (_cacheTimer != null)
            {
                _cacheTimer.Close();
            }
            _cacheTimer = new Timer(_cacheSeconds);
            _cacheTimer.Start();
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