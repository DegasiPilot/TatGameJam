using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnDie : MonoBehaviour
{
    public string Name;

    private void OnDestroy()
    {
        EventManager.Lose(Name + " потерял все жизни");
    }
}
