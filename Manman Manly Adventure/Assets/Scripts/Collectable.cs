using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("References")]
    public GameObject visuals;
    public GameObject particles;

    public CircleCollider2D collectableCollider2D;
    public AudioSource collectSound;

    public bool isSecret;

    // Start is called before the first frame update
    void Start()
    {
        collectableCollider2D = GetComponent<CircleCollider2D>();
        collectSound = GetComponent<AudioSource>();
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
            destroyThis();
        }
    }

    //destroys game object in a way that allows it to play a sound
    public void destroyThis()
    {
        collectableCollider2D.enabled = false;
        visuals.SetActive(false);
        particles.SetActive(true);

        collectSound.Play();
        Destroy(gameObject, collectSound.clip.length);
    }
}
