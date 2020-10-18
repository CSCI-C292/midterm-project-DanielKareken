using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("References")]
    public GameObject gemVisuals;
    //public GameObject collectedParticleSystem;
    public CircleCollider2D gemCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D theCollider)
    {
        if (theCollider.CompareTag("Player"))
        {
            //gemCollider2D.enabled = false;
            //gemVisuals.SetActive(false);
            //collectedParticleSystem.SetActive(true);
            //Invoke("DeactivateGemGameObject", durationOfCollectedParticleSystem);
            gameObject.SetActive(false);
        }
    }
}
