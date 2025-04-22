using System;
using affiliation;
using character.attribute;
using character.moving;
using common.stateMachine;
using DefaultNamespace;
using UI;
using ui.frame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Attribute = System.Attribute;
using IState = common.stateMachine.IState;

[AttributeUsage(AttributeTargets.Property)]
public class DependencyAttribute : Attribute
{
    public DependencyAttribute(Type type)
    {
    }
}

namespace character.solider
{
    public class Solider : Character
    {
        private StateMachine<MotionStatusEnum> _stateMachine = new StateMachine<MotionStatusEnum>();
        [Binding("_attackRange")] public RangeTrigger attackRange { get; set; }
        [Binding("_selfRange")] public RangeTrigger selfRange { get; set; }
        private SoliderUI _soliderUI;
        private MovingController _movingController;
        public new CharacterAttribute characterAttribute {
            get => base.characterAttribute;
            set
            {
                base.characterAttribute = value;
                _soliderUI.health =(HealthPoints)getAttribute(AttributeEnum.HP);
            } 
        }

        public override void initialize()
        {
            base.initialize();
            attackRange.onEnterRange += onEnterAttackRange;
            selfRange.onEnterRange += onEnterSelfRange;
            _stateMachine.addState(MotionStatusEnum.IDLE, new IdleState(gameObject.GetComponent<Animator>()));
            _stateMachine.addState(MotionStatusEnum.MOVING,
                new MoveState(gameObject.GetComponent<Animator>(), _movingController));
            switchMotionStatus(MotionStatusEnum.IDLE);
        }
        
        public void setMovingController(MovingController movingController)
        {
            _movingController = movingController;
        }

        public void setSoliderUI(SoliderUI soliderUI)
        {
            _soliderUI = soliderUI;
            if (gameObject == null)
            {
                Debug.LogWarning("gameObject is null, please set gameObject first!");
                return;
            }
            _soliderUI.follow(gameObject.gameObject.transform);
        }

        private void onEnterAttackRange(Collider2D other)
        {
        }

        private void onEnterSelfRange(Collider2D other)
        {
        }
        
        public void OnDestroy()
        {
            Locator.fetch<SoliderManager>().removeSolider(this);
        }

        public override void switchMotionStatus(MotionStatusEnum motionStatusEnum)
        {
            _stateMachine.switchState(motionStatusEnum);
        }

        public MotionStatusEnum getCurrentState()
        {
            return _stateMachine.currentStateType;
        }

        public override void update()
        {
            _stateMachine.update();
        }
    }
}