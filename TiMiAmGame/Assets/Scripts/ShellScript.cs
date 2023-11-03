using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    private float flySpeed;
    private Vector2 endPos;
    private float damage;
    private UnitScript creator;

    private UnitScript unit;

    public void SetUp(float flySpeed, Vector2 targerPos, float damage, UnitScript creator)
    {
        this.flySpeed = flySpeed;
        endPos = targerPos;
        this.damage = damage;
        this.creator = creator;
        StartCoroutine(Launch());
    }

    public IEnumerator Launch()
    {
        while ((Vector2)transform.position != endPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, flySpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out unit) && unit != creator) {
            StopAllCoroutines();
            unit.GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
