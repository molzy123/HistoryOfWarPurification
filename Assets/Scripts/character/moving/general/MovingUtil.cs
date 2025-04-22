using UnityEngine;

namespace character.moving
{
    public class MovingUtil
    {

        public static Vector3 getDirectionByType(DirectionEnum directionEnum)
        {
            switch (directionEnum)
            {
                case DirectionEnum.UP:
                    return Vector3.up;
                case DirectionEnum.DOWN:
                    return Vector3.down;
                case DirectionEnum.LEFT:
                    return Vector3.left;
                case DirectionEnum.RIGHT:
                    return Vector3.right;
                default:
                    return Vector3.zero;
            }
        }
        
    }
}