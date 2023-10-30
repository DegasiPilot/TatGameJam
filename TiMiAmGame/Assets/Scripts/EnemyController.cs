using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Speed;
    public double AttackDistance;
    public double Damage;
    public float RechargeTime;

    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool attackReady;

    public void SetUp(GameObject player)
    {
        this.player = player;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        attackReady = true;
    }

    private void FixedUpdate()
    {
        Vector2 playerRelativePos = player.transform.position - transform.position;
        if (playerRelativePos.magnitude <= AttackDistance)
        {
            rb.velocity = Vector2.zero;
            if(attackReady)
                Attack(player.GetComponent<UnitScript>()); 
        }
        else
        {
            rb.velocity = (player.transform.position - transform.position).normalized * Speed;
        }
        flip(playerRelativePos.x);
    }

    private void Attack(UnitScript unit)
    {
        unit.GetDamage(Damage);
        attackReady = false;
        StartCoroutine(Recharge());
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
