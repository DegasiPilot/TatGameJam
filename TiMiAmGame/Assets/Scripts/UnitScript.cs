using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public double HP;

    public void GetDamage(double damage)
    {
        HP -= damage;
        if (HP <= 0)
            gameObject.SetActive(false);
    }
}
