using System.Collections;
using System.Collections.Generic;
using affiliation;
using DefaultNamespace;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    private int _attackValue = 5;
    private int _attackRange = 10;
    private float _coolDown;
    private affiliation.Affiliation _affiliation;

    public int AttackRange
    {
        get => _attackRange;
        set => _attackRange = value;
    }
    // Update is called once per frame
    void Update()
    {
        if (_coolDown > 0f)
        {
            _coolDown -= Time.deltaTime;
        }

        if (CanAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        _coolDown = 1;
        GameObject bullet = BulletFactory.createBullet();
        bullet.transform.position = gameObject.transform.position;
        bullet.GetComponent<BulletMoveController>().Target = getNearestAttackTarget();
        bullet.GetComponent<BulletMoveController>().Source = gameObject;
        bullet.GetComponent<BulletMoveController>().ATK = _attackValue;
    }
    
    public GameObject getNearestAttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AttackRange);
        float minimumDistance = float.MaxValue;
        GameObject result = null;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("GameObject") && Locator.fetch<AffiliationManager>().canAttack(_affiliation, collider.GetComponent<AttackController>()._affiliation) )
            {
                if (Vector3.Distance(transform.position, collider.transform.position) < minimumDistance)
                {
                    minimumDistance = Vector3.Distance(gameObject.transform.position, transform.position);
                    result = collider.gameObject;
                }
            }
        }
        return result;
    }
    
    public bool CanAttack()
    {
        if (_coolDown > 0)
        {
            return false;
        }

        return true;
    }
}
