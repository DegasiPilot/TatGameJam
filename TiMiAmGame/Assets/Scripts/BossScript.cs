using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float Speed;
    public float AttackDistance;
    public float Damage;
    public float RechargeTime;
    public GameObject Shell;
    public float AnimSpeed;

    private PlayerController player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool attackReady;
    private BoxCollider2D trigger;
    private GameObject target;
    private Vector2 targetRelativePos;

    public void SetUp(PlayerController player)
    {
        this.player = player;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        attackReady = true;
        trigger = GetComponent<BoxCollider2D>();
        trigger.enabled = false;
        target = player.gameObject;
    }

    private void FixedUpdate()
    {
        targetRelativePos = player.transform.position - transform.position;
        if (targetRelativePos.magnitude <= AttackDistance)
        {
            rb.velocity = Vector2.zero;
            if (attackReady)
                StartCoroutine(Attack(targetRelativePos.normalized));
        }
        else
        {
            rb.velocity = (target.transform.position - transform.position).normalized * Speed;
        }
        flip(targetRelativePos.x);
    }

    private IEnumerator Attack(Vector3 direction)
    {
        trigger.enabled = true;
        Vector3 EndPos = direction * AttackDistance;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ);
        GameObject currentShell = Instantiate(Shell, transform.position + direction, rotation, transform);
        attackReady = false;
        while (currentShell.transform.localPosition != EndPos)
        {
            currentShell.transform.localPosition = Vector2.MoveTowards(currentShell.transform.localPosition, EndPos, AnimSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        trigger.enabled = false;
        StartCoroutine(Recharge());
        attackReady = false;
        StartCoroutine(Recharge());
    }

    public void shellHit(UnitScript unit)
    {
        unit.GetDamage(Damage);
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