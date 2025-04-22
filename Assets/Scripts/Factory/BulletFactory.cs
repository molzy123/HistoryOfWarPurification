using UnityEngine;

namespace DefaultNamespace
{
    public class BulletFactory
    {
        public static GameObject createBullet()
        {
            GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), Vector3.zero, Quaternion.identity);
            return bullet;
        }
    }
}