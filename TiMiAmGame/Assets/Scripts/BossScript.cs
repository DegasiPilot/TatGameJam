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
    public float shellSpeed;

    private PlayerController player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private UnitScript unitScript;
    private bool attackReady;
    private Vector2 targetRelativePos;
    private GameManager gameManager;

    private Vector2 shellOffset;
    private Vector2 shellEndPos;
    private float shellRotZ;
    private Quaternion shellRotation;
    private Vector2 shellPosition;

    public void SetUp(PlayerController player, GameManager gameManager)
    {
        this.player = player;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        attackReady = true;
        shellOffset = GetComponent<CapsuleCollider2D>().size / 2;
        unitScript = GetComponent<UnitScript>();
        this.gameManager = gameManager;
    }

    private void FixedUpdate()
    {
        targetRelativePos = player.transform.position - transform.position;
        if (targetRelativePos.magnitude <= AttackDistance)
        {
            rb.velocity = Vector2.zero;
            if (attackReady)
                Attack(targetRelativePos.normalized);
        }
        else
        {
            rb.velocity = targetRelativePos.normalized * Speed;
        }
        flip(targetRelativePos.x);
    }

    private void Attack(Vector2 direction)
    {
        shellEndPos = (Vector2)transform.position + direction * AttackDistance;
        shellRotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shellRotation = Quaternion.Euler(0f, 0f, shellRotZ + 180);
        shellPosition = (Vector2)transform.position + direction * shellOffset;
        GameObject currentShell = Instantiate(Shell, shellPosition, shellRotation);
        currentShell.GetComponent<ShellScript>().SetUp(shellSpeed, shellEndPos, Damage, unitScript);
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

    public void OnDestroy()
    {
        EventManager.OnBossSlain.Invoke();
    }
}