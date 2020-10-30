using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float shotSpeed = 10;
    public float ammo = 0f;

    public bool hasWeapon = false;

    [SerializeField] Animator animator;
    [SerializeField] RuntimeData runtimeData;

    // Use this for initialization
    void Awake()
    {
        runtimeData.animationLock = false;
        runtimeData.facingRight = false;
        GameEvents.health = 100;

        GameEvents.GameOver += OnGameOver;
    }

    private void Update()
    {
        //basic attack
        if (Input.GetMouseButtonDown(0) && runtimeData.animationLock == false)
        {
            runtimeData.animationLock = true;
            animator.SetTrigger("Attack");
        }

        //ability1 attack
        if (Input.GetMouseButtonDown(1) && runtimeData.animationLock == false)
        {
            runtimeData.animationLock = true;
            animator.SetTrigger("ability01");
        }

        //close the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        /*if (Input.GetKeyDown(KeyCode.S) && hasWeapon)
        {
            GameObject projectile = Instantiate(projectilePrefab, shotSpawn.transform.position, projectilePrefab.transform.rotation);
            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            float directedSpeed = shotSpeed;

            //check which direction player is facing to shoot forward
            if (spriteRenderer.flipX == true)
            {
                directedSpeed = -directedSpeed;
            }

            projectileRB.velocity = transform.right * directedSpeed;
            ammo--;

            if (ammo < 1)
            {
                hasWeapon = false;
            }
        }*/

        if (runtimeData.strengthUp == true)
        {
            //activate animation
            //print("Strength boosted");
        }

        VelocityRefresh();
        //print(runtimeData.animationLock);
    }

    protected override void VelocityRefresh()
    {
        base.VelocityRefresh();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StageBounds"))
        {
            GameEvents.InvokeGameOver("died");
        }
        else if (other.gameObject.CompareTag("EnemyAttackHitbox"))
        {
            GameEvents.health -= 20;
            runtimeData.animationLock = true;
            animator.SetBool("hurt", runtimeData.animationLock);
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if touching an enemy
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Hazard"))
        {
            GameEvents.health -= 10;
            runtimeData.animationLock = true;
            animator.SetBool("hurt", runtimeData.animationLock);
        }
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded && !runtimeData.animationLock)
        {
            velocity.y = jumpTakeOffSpeed;
        }

        else if (Input.GetButtonUp("Jump") && !runtimeData.animationLock)
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        //change to face direction moving
        if (move.x > 0.01f)
        {
            if (runtimeData.facingRight == false)
            {
                runtimeData.facingRight = true;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
        else if (move.x < -0.01f)
        {
            if (runtimeData.facingRight == true)
            {
                runtimeData.facingRight = false;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        if (runtimeData.speedUp == true)
        {
            targetVelocity = move * maxSpeed * 1.5f;
        }
        else {
            targetVelocity = move * maxSpeed;
        }
    }

    void OnGameOver(object sender, EventArgs args)
    {
        gameObject.SetActive(false);
        Invoke("RestartLevel", 2f);
    }

    //reloads level after player dies, timer expires, or score limit reached
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}