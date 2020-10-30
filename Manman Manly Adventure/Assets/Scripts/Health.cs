using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public RuntimeData runtimeData;

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Health: " + GameEvents.health;

        if (GameEvents.health <= 0)
        {
            GameEvents.InvokeGameOver("died");
        }

        //healthUp mechanic
        if (runtimeData.healthUp && GameEvents.health < 76f)
        {
            GameEvents.health += 25f;
        }
        else if (runtimeData.healthUp && GameEvents.health != 100)
        {
            GameEvents.health = 100;
        }
    }
}
