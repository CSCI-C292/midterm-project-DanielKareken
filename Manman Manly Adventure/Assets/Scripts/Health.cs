using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] RuntimeData runtimeData;
    Slider slider;

    float health;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (GameEvents.health <= 0)
        {
            runtimeData.winCond = "died";
            runtimeData.gameOver = true;
        }

        health = GameEvents.health;
        setHealth(health);
    }

    //HealthUp power up
    public void OnPowerUp(object sender, PowerUpEventArgs args)
    {
        if (args.powerUp == "Health")
        {
            GameEvents.health = 100;
        }
        
    }

    public void setHealth(float health)
    {
        slider.value = health;
    }
}
