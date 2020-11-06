using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimers : MonoBehaviour
{
    public RuntimeData runtimeData;

    bool gameTimerOn;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.powerUpTimer = 0;
        runtimeData.powerUpActive = false;
        gameTimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //PowerUp Timer
        if (GameEvents.powerUpTimer > 0 && runtimeData.powerUpActive)
        {
            GameEvents.powerUpTimer -= Time.deltaTime * 1f;
            print(GameEvents.powerUpTimer);
        }

        else if (GameEvents.powerUpTimer <= 0 && runtimeData.powerUpActive)
        {
            runtimeData.powerUpActive = false;
            GameEvents.InvokePowerDown();
        }

        //Game Timer
        if (GameEvents.gameTimer > 0)
        {
            GameEvents.gameTimer -= Time.deltaTime * 1f;
        }
        else if (GameEvents.gameTimer <= 0 && gameTimerOn)
        {
            //end game
            gameTimerOn = false;
            print("calling (arg times up)");
            runtimeData.winCond = "times up";
            runtimeData.gameOver = true;
        }

        //Ability timer
        if (GameEvents.abilityCooldown > 0)
        {
            GameEvents.abilityCooldown -= Time.deltaTime * 1f;
        }
    }
}
