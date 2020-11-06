using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] RuntimeData runtimeData;
    [SerializeField] GameObject player;

    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    { 
        runtimeData.pacifist = true;
        runtimeData.gameOver = false;
        runtimeData.winCond = "";
    }

    // Update is called once per frame
    void Update()
    {
        //close the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (runtimeData.gameOver && !gameOver)
        {
            OnGameOver();
            gameOver = true;
        }

        if(GameEvents.secretsCollected == 3 && !gameOver)
        {
            runtimeData.gameOver = true;
            runtimeData.winCond = "secret";
        }
    }

    //disable necessary properties
    void OnGameOver()
    {
        print("disable player");
        player.SetActive(false);
    }
}
