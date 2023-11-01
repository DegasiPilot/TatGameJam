using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    UnitScript unit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out unit)) {
            transform.parent.GetComponent<BossScript>().shellHit(unit);
        }
    }
}
