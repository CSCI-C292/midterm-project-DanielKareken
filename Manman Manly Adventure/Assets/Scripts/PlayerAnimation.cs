using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject swordHitBox;
    public GameObject fireball;
    public GameObject player;

    Vector2 fireballStartPos;
    float fireballSpeed = 20f;
    bool fireballActive;
    bool facingLeft;

    [SerializeField] RuntimeData runtimeData;
    [SerializeField] AudioSource swordSound;
    [SerializeField] AudioSource fireballSound;

    void Awake()
    {
        swordHitBox.SetActive(false);
        fireball.SetActive(false);
        facingLeft = true;
    }

    void Update()
    {
        //For abilities
        if (GameEvents.abilityCooldown > 0)
        {
            Vector3 moveFireball = fireball.transform.up;
            fireball.transform.position += moveFireball * fireballSpeed * Time.deltaTime;
        }
        else if (GameEvents.abilityCooldown <= 0 && fireballActive)
        {
            fireballActive = false;
            fireball.SetActive(false);
        }
    }

    void unlockPlayer()
    {
        runtimeData.animationLock = false;
    }

    void notHurt()
    {
        runtimeData.hurt = false;
    }

    void attackHitboxEnable()
    {
        swordHitBox.SetActive(true);
    }

    void attackHitboxDisable() {
        swordHitBox.SetActive(false);
    }

    void fireballEnable()
    {
        fireball.SetActive(true);
        fireballActive = true;

        if (runtimeData.facingRight && facingLeft)
        {
            facingLeft = false;
            fireball.transform.eulerAngles = new Vector3(0, 180, 90);
        }
        else if (!runtimeData.facingRight && !facingLeft)
        {
            facingLeft = true;
            fireball.transform.eulerAngles = new Vector3(0, 0, 90);
        }

        fireballStartPos = player.transform.position;
        Vector2 displaceFromPlayer = fireball.transform.up;
        fireball.transform.position = fireballStartPos + displaceFromPlayer * 2;
        GameEvents.abilityCooldown = .7f;
    }

    void playSwordSound()
    {
        swordSound.Play();
    }

    void playFireballSound()
    {
        fireballSound.Play();
    }
}
