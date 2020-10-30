using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimers : MonoBehaviour
{
    public RuntimeData runtimeData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //PowerUp Timer
        if (GameEvents.powerUpTimer > 0)
        {
            GameEvents.powerUpTimer -= Time.deltaTime * 1f;
            print(GameEvents.powerUpTimer);
        }
        else if (GameEvents.powerUpTimer <= 0)
        {
            runtimeData.speedUp = false;
            runtimeData.strengthUp = false;
            runtimeData.healthUp = false;
            runtimeData.noActive = true;
        }

        //Game Timer
        if (GameEvents.gameTimer > 0)
        {
            GameEvents.gameTimer -= Time.deltaTime * 1f;
        }
        else if (GameEvents.gameTimer <= 0)
        {
            //end game
            GameEvents.InvokeGameOver("times up");
        }

        //Ability timer
        if (GameEvents.abilityTimer > 0)
        {
            GameEvents.abilityTimer -= Time.deltaTime * 1f;
        }
    }
}
