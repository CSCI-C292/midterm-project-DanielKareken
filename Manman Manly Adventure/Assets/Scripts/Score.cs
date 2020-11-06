using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    float score;

    private void Update()
    {
        GetComponent<Text>().text = "Score: " + GameEvents.score;
    }
}
