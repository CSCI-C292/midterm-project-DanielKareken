using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    float health;

    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Health: " + GameEvents.health;
    }
}
