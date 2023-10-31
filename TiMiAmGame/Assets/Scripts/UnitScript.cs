using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitScript : MonoBehaviour
{
    public float MaxHP;
    public Slider healthBar;

    [HideInInspector] float currentHP;
    private SpriteRenderer spriteRenderer;
    private bool isPlayer;

    public void SetUp(bool isPlayer)
    {
        currentHP = MaxHP;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (healthBar != null) {
            healthBar.maxValue = MaxHP;
            healthBar.value = currentHP;
        }
        this.isPlayer = isPlayer;
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

    private IEnumerator DamageAnim()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
    }

    private void Death()
    {
        if (isPlayer)
            Time.timeScale = 0;
        Destroy(gameObject);
    }
}