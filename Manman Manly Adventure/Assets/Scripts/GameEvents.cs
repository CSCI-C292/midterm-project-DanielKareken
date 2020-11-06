using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEventArgs : EventArgs
{
    public string condition;
}

public class PowerUpEventArgs : EventArgs
{
    public string powerUp;
}

public static class GameEvents
{
    public static event EventHandler<PowerUpEventArgs> PowerUp;
    public static event EventHandler PowerDown;

    public static float health;

    public static int secretsCollected;
    public static int score;

    public static float powerUpTimer;

    public static float gameTimer;
    public static bool gameOver;

    public static float abilityCooldown;

    public static void InvokePowerUp(string power)
    {
        PowerUp(null, new PowerUpEventArgs { powerUp = power });
    }

    public static void InvokePowerDown()
    {
        PowerDown(null, EventArgs.Empty);
    }
}
