using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Music
{
    public float Speed;
    public double AttackDistance;
    public float Damage;
    public float RechargeTime;

    private PlayerController player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool attackReady;
    private GameObject camp;

    public void SetUp(PlayerController player, GameObject camp)
    {
        this.player = player;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        attackReady = true;
        this.camp = camp;
    }

    private void FixedUpdate()
    {
        Vector2 playerPos = player.transform.position - transform.position;
        Vector2 campPos = camp.transform.position - transform.position;
        GameObject target = playerPos.magnitude <= campPos.magnitude ? player.gameObject : camp;
        Vector2 targetRelativePos = target == player.gameObject ? playerPos : campPos;
        if (targetRelativePos.magnitude <= AttackDistance)
        {
            rb.velocity = Vector2.zero;
            if(attackReady)
                Attack(target.GetComponent<UnitScript>()); 
        }
        else
        {
            rb.velocity = (target.transform.position - transform.position).normalized * Speed;
        }
        flip(targetRelativePos.x);
    }

    private void Attack(UnitScript unit)
    {
        unit.GetDamage(Damage);
        attackReady = false;
        StartCoroutine(Recharge());
        PlaySound(objsound[0]);
    }

    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(RechargeTime);
        attackReady = true;
    }

    private void flip(float x)
    {
        if (x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
