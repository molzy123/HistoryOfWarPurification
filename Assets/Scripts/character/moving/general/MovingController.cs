using character.attribute;
using UnityEngine;

namespace character.moving
{
    public class MovingController
    {
        private DirectionEnum _direction;

        private Attribute _spd;

        private Transform _transform;
        
        public MovingController(DirectionEnum direction, Attribute spd, Transform transform)
        {
            _direction = direction;
            _spd = spd;
            _transform = transform;
        }
        
        public void switchDirection(DirectionEnum direction)
        {
            _direction = direction;
        }

        public void update()
        {
             _transform.position += MovingUtil.getDirectionByType(_direction) * _spd.value * Time.deltaTime;
        }
        
    }
}