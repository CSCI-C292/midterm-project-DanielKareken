using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    float score;

    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + GameEvents.score;
    }
}
