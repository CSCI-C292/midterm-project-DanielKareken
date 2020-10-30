using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public RuntimeData runtimeData;

    public GameObject healthIcon;
    public GameObject speedIcon;
    public GameObject strengthIcon;

    public void Start()
    {
        speedIcon.SetActive(false);
        strengthIcon.SetActive(false);
        healthIcon.SetActive(false);
    }

    private void Update()
    {
        if (runtimeData.speedUp)
        {
            speedIcon.SetActive(true);
            strengthIcon.SetActive(false);
            healthIcon.SetActive(false);
        }
        else if (runtimeData.healthUp)
        {
            speedIcon.SetActive(false);
            strengthIcon.SetActive(false);
            healthIcon.SetActive(true);
        }
        else if (runtimeData.strengthUp)
        {
            speedIcon.SetActive(false);
            strengthIcon.SetActive(true);
            healthIcon.SetActive(false);
        }
        else if (runtimeData.noActive)
        {
            speedIcon.SetActive(false);
            strengthIcon.SetActive(false);
            healthIcon.SetActive(false);
        }
    }
}
