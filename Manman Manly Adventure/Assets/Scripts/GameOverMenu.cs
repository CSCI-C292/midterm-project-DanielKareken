using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] Text gameOverText;
    [SerializeField] Text condition;
    [SerializeField] Text score;
    [SerializeField] RuntimeData runtimeData;

    Color textColor = Color.red;

    bool gameEnded = false;

    void Update()
    {
        if (runtimeData.gameOver && !gameEnded)
        {
            OnGameOver(runtimeData.winCond);
            gameEnded = true;
        }

        gameOverText.color = textColor;
    }

    void OnGameOver(string cond)
    {
        print("OnGameOver successfully called");
        string rank = "";

        if (cond == "died")
        {
            rank = "Daisy Pusher";
        }

        else if (cond == "times up")
        {
            if (GameEvents.score < 70)
            {
                rank = "Wimp";
            }

            else if (runtimeData.pacifist && GameEvents.score >= 40)
            {
                rank = "Beta Pacifist";
                textColor = Color.green;
            }
        }

        else if (cond == "Other")
        {
            if (GameEvents.score >= 500 && !GameEvents.bossBeaten)
            {
                rank = "Manly man";
                textColor = Color.green;
            }

            else if (runtimeData.pacifist && GameEvents.score >= 60)
            {
                rank = "True Pacifist";
                textColor = Color.green;
            }

            else if (!runtimeData.pacifist && GameEvents.score >= 60)
            {
                rank = "Based Scrooge";
                textColor = Color.green;
            }

            else if (!runtimeData.pacifist && GameEvents.bossBeaten)
           {
                rank = "Manliest Man";
                textColor = Color.green;
            }
        }

        else if (cond == "secret")
        {
            rank = "Treasure Hunter";
            textColor = Color.green;
        }

        condition.text = "Manliness Rank = " + rank;
        score.text = "Final Score = " + GameEvents.score;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
