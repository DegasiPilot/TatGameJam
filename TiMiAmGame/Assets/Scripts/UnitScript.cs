using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public double MaxHP;

    [HideInInspector] double currentHP;
    private SpriteRenderer spriteRenderer;

    public void SetUp()
    {
        currentHP = MaxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void GetDamage(double damage)
    {
        currentHP -= damage;
        StartCoroutine(DamageAnim());
        if (currentHP <= 0)
            gameObject.SetActive(false);
    }

    private IEnumerator DamageAnim()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
    }
}