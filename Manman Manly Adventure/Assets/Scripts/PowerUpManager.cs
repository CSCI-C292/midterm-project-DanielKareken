using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public RuntimeData runtimeData;

    public GameObject speedIcon;
    public GameObject strengthIcon;

    public void Start()
    {
        speedIcon.SetActive(false);
        strengthIcon.SetActive(false);

        GameEvents.PowerUp += OnPowerUp;
        GameEvents.PowerDown += OnPowerDown;
    }

    void OnPowerUp(object sender, PowerUpEventArgs args)
    {
        if (args.powerUp == "Strength")
        {
            strengthIcon.SetActive(true);
            speedIcon.SetActive(false);
        }
        else if (args.powerUp == "Speed")
        {
            speedIcon.SetActive(true);
            strengthIcon.SetActive(false);
        }
    }

    void OnPowerDown(object sender, EventArgs args)
    {
        speedIcon.SetActive(false);
        strengthIcon.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.PowerUp -= OnPowerUp;
        GameEvents.PowerDown -= OnPowerDown;
    }
}
