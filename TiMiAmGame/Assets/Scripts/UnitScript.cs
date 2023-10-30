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

    public void SetUp()
    {
        currentHP = MaxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (healthBar != null) {
            healthBar.maxValue = MaxHP;
            healthBar.value = currentHP;
        }
    }

    public void GetDamage(float damage)
    {
        currentHP -= damage;
        StartCoroutine(DamageAnim());
        if (currentHP <= 0)
            Death();
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
        gameObject.SetActive(false);
    }
}