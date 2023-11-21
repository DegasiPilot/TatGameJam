using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public BonusType Type;

    public float BonusPower;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            UseBonusOn(player);
            Destroy(gameObject);
        }
    }

    private void UseBonusOn(PlayerController player)
    {
        switch (Type)
        {
            case BonusType.Heal:
                player.unitScript.Heal(BonusPower);
                break;
        } 
    }
}
