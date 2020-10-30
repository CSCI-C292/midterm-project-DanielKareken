using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEventArgs : EventArgs
{
    public string condition;
}

public static class GameEvents
{
    public static event EventHandler GameOver;

    public static float health;

    public static int secretsCollected = 0;
    public static int score = 0;

    public static float powerUpTimer = 0f;

    public static float gameTimer = 360f;
    public static bool gameOver = false;

    public static float abilityTimer = 0f;

    public static void InvokeGameOver(string cond)
    {
        GameOver(null, new GameOverEventArgs { condition = cond });
    }
}
