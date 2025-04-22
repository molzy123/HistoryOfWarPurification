using DefaultNamespace;
using GameCore;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public abstract class FollowUI : UIBase, IFollowUI, IUpdate
    {

        private Transform _followTarget;

        public  void initialize()
        {
            Locator.fetch<UpdateService>().addHandler(this);
        }

        public void follow(Transform followTarget)
        {
            _followTarget = followTarget;
        }

        public virtual void update()
        {
            if (_followTarget != null)
            {
                // Using Camera.main here works for any setup but has some performance considerations.
                Vector2 namePos = Camera.main.WorldToScreenPoint(_followTarget.position);
                gameObject.transform.position = namePos + new Vector2(-100, 50);
            }
            
        }

        private void OnDestroy()
        {
            _followTarget = null;
            Locator.fetch<UpdateService>().removeHandler(this);
        }
        
    }
}