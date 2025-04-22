using System;
using affiliation;
using character.attribute;
using character.moving;
using DefaultNamespace;
using game_core;
using GameCore;
using UI;
using UnityEditor.Animations;
using UnityEngine;
using Attribute = character.attribute.Attribute;

namespace character
{
    public abstract class Character : GameObjectSolid, IMoving, IUpdate 
    {
        public Guid id;
        public AffiliationEnum affiliation { get; set; }
        protected CharacterAttribute characterAttribute { get; set; }
        
        private AnimationController _animationController;

        public override void initialize()
        {
            id = Guid.NewGuid();
            this.affiliation = affiliation;
            _animationController = new AnimationController(gameObject.GetComponent<Animator>());
            
            Locator.fetch<UpdateService>().addHandler(this);
        }
        
        public void OnDestroy()
        {
            Locator.fetch<UpdateService>().removeHandler(this);
        }

        public abstract void update();

        public abstract void switchMotionStatus(MotionStatusEnum motionStatusEnum);
        public bool isLive()
        {
            return characterAttribute.isLessThanMinValue(AttributeEnum.HP);
        }
        
        public Attribute getAttribute(AttributeEnum attributeEnum)
        {
            return characterAttribute.getAttribute(attributeEnum);
        }
    }
}