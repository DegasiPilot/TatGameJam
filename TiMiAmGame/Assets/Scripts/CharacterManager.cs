using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Sprite[] HeroesSprites;

    private void Start()
    {
        SetCharacter(0);
    }

    public void SetCharacter(int id)
    {
        GeneralData.HeroSprite = HeroesSprites[id];
    }
}
