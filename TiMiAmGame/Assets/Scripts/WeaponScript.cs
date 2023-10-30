using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [HideInInspector] public bool attackReady;

    public float AttackDistance;
    public float Damage;
    public float RechargeTime;
    public float AnimSpeed;

    private BoxCollider2D trigger;

    public void SetUp()
    {
        attackReady = true;
        trigger = GetComponent<BoxCollider2D>();
        trigger.enabled = false;
    }

    public IEnumerator Attack(Vector2 direction)
    {
        trigger.enabled = true;
        Vector3 EndPos = direction * AttackDistance;
        while (transform.localPosition != EndPos)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, EndPos, AnimSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = Vector2.zero;
        attackReady = false;
        trigger.enabled = false;
        StartCoroutine(Recharge());
    }

    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(RechargeTime);
        attackReady = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnitScript unit;
        if(collision.TryGetComponent(out unit))
        {
            unit.GetDamage(Damage);
        }
    }
}
