using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject[] interfaces;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.GameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGameOver(object sender, EventArgs args)
    {
        for (int i = 0; i < interfaces.Length; i++)
        {
            interfaces[i].SetActive(false);
        }
    }
}
