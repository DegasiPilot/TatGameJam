using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitScript : Music
{
    public float MaxHP;
    public Slider healthBar;
    public float DropChanse;
    public GameObject[] DroppingBonuses;

    [HideInInspector] float currentHP;
    private SpriteRenderer spriteRenderer;
    private bool isPlayer;
    private PlayerController player;
    private bool isBoss;
    private BossScript boss;

    public void SetUp()
    {
        currentHP = MaxHP;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (healthBar != null)
        {
            healthBar.maxValue = MaxHP;
            healthBar.value = currentHP;
        }
        isPlayer = TryGetComponent(out player);
        isBoss = TryGetComponent(out boss);
    }

    public void GetDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            Death();
        StartCoroutine(DamageAnim());
        if (healthBar != null)
            healthBar.value = currentHP;
    }

    public void Heal(float HP)
    {
        if (HP == -1)
            currentHP = MaxHP;
        else
            currentHP += HP;
        if (healthBar != null)
            healthBar.value = currentHP;
    }

    private IEnumerator DamageAnim()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
    }

    private void Death()
    {
        if (isPlayer)
        {
            PlaySound(objsound[1]);
        }
        else if (isBoss)
        {
            boss.OnSlain();
        }

        if (DroppingBonuses != null)
        {
            if (Random.value <= DropChanse)
            {
                int index = Random.Range(0, DroppingBonuses.Length);
                Instantiate(DroppingBonuses[index], transform.position, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}