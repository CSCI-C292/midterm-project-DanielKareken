using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Collectable
{
    public string type;
    public RuntimeData runtimeData;

    // Start is called before the first frame update
    void Start()
    {
        runtimeData.strengthUp = false;
        runtimeData.speedUp = false;
        runtimeData.healthUp = false;
        runtimeData.noActive = true;

        collectableCollider2D = GetComponent<CircleCollider2D>();
        collectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        visuals.transform.Rotate(0, 8, 0);
    }

    void OnTriggerEnter2D(Collider2D theCollider)
    {
        if (theCollider.CompareTag("Player"))
        {
            //Health PowerUp
            if (type == "Health")
            {
                runtimeData.healthUp = true;
                runtimeData.noActive = false;
                GameEvents.powerUpTimer = 3;
            }
            //Strength PowerUp
            else if (type == "Strength")
            {
                runtimeData.strengthUp = true;
                runtimeData.noActive = false;
                GameEvents.powerUpTimer = 10f;
            }
            //Speed PowerUp
            else if (type == "Speed")
            {
                runtimeData.speedUp = true;
                runtimeData.noActive = false;
                GameEvents.powerUpTimer = 15f;
            }

            destroyThis();
        }
    }
}
