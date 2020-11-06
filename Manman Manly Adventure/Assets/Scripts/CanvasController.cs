using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject[] interfaces;
    [SerializeField] RuntimeData runtimeData;

    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        interfaces[interfaces.Length - 1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (runtimeData.gameOver && !gameOver)
        {
            OnGameOver();
            gameOver = true;
        }
    }

    void OnGameOver()
    {
        for (int i = 0; i < interfaces.Length - 1; i++)
        {
            interfaces[i].SetActive(false);
        }

        interfaces[4].SetActive(true);
    }
}
