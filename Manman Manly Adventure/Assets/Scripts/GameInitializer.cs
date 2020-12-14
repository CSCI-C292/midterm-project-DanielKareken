using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] AudioSource themesong;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.secretsCollected = 0;
        GameEvents.score = 0;

        GameEvents.powerUpTimer = 0f;

        GameEvents.gameTimer = 180f;
        GameEvents.gameOver = false;

        GameEvents.abilityCooldown = 0f;
    }

    void Start()
    {
        themesong.Play();
        player.SetActive(true);
    }
}
