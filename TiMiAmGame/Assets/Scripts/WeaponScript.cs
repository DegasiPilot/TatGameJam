using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [HideInInspector] public bool attackReady;

    public float HoldDistance;
    public float AttackDistance;
    public float Damage;
    public float RechargeTime;
    public float AnimSpeed;

    private BoxCollider2D trigger;
    private bool blocked;
    private GameObject obstacles;

    public void SetUp(GameObject obstacles)
    {
        attackReady = true;
        trigger = GetComponent<BoxCollider2D>();
        trigger.enabled = false;
        blocked = false;
        this.obstacles = obstacles;
    }

    public IEnumerator Attack(Vector2 direction)
    {
        trigger.enabled = true;
        Vector3 EndPos = direction * AttackDistance;
        while (transform.localPosition != EndPos && !blocked)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, EndPos, AnimSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        blocked = false;
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
        if(collision.transform.parent == obstacles)
        {
            blocked = true;
            return;
        }

        UnitScript unit;
        if(collision.TryGetComponent(out unit))
        {
            unit.GetDamage(Damage);
        }
    }
}
