using character;
using DefaultNamespace;
using UnityEngine;

public class BulletMoveController : MonoBehaviour
{

    public GameObject Target;
    public GameObject Source;
    public int ATK = 1;
    public float speed = 30;

    private AffiliationEnum _sourceType;
    
    // Start is called before the first frame update
    void Start()
    {
        _sourceType = Source.GetComponent<affiliation.Affiliation>().affiliation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Target) // 判断碰撞的物体是否为目标物体
        {
            // if (other.GetComponent<IHealth>() != null)
            {
                // other.GetComponent<IHealth>().health.decreaseBlood(ATK);
            }
           // 处理碰撞后的逻辑，例如销毁子弹、造成伤害等
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 direction = Target.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance > 0.1f) // 当距离大于一定值时，继续移动
        {
            direction.Normalize(); // 将方向向量归一化
            transform.Translate(direction * speed * Time.deltaTime); // 移动gameObject
        }
        
    }
}
