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
        collectableCollider2D = GetComponent<CircleCollider2D>();
        collectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        visuals.transform.Rotate(0, 4, 0);
    }

    void OnTriggerEnter2D(Collider2D theCollider)
    {
        if (theCollider.CompareTag("Player"))
        {
            GameEvents.InvokePowerUp(type);
            runtimeData.powerUpActive = true;
            destroyThis();
        }
    }
}
