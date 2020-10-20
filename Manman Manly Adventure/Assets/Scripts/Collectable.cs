using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("References")]
    public GameObject visuals;
    public GameObject particles;

    CircleCollider2D collectableCollider2D;
    AudioSource collectSound;

    public bool isSecret;

    // Start is called before the first frame update
    void Start()
    {
        collectableCollider2D = GetComponent<CircleCollider2D>();
        collectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D theCollider)
    {
        if (theCollider.CompareTag("Player"))
        {
            if (isSecret)
            {
                GameEvents.secretsCollected += 1;
            }

            GameEvents.score += 10;

            collectableCollider2D.enabled = false;
            visuals.SetActive(false);
            particles.SetActive(true);

            collectSound.Play();
            Destroy(gameObject, collectSound.clip.length);
            gameObject.SetActive(false);
        }
    }
}
