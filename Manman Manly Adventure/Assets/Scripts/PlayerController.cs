using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float shotSpeed = 10;
    public float ammo = 0f;

    public bool died = false;

    public Animator animator;
    [SerializeField] RuntimeData runtimeData;

    Vector3 baseScale;
    float baseSpeed;

    // Use this for initialization
    void Awake()
    {
        runtimeData.animationLock = false;
        runtimeData.hurt = false;
        runtimeData.facingRight = false;
        GameEvents.health = 100;
        baseScale = transform.localScale;
        baseSpeed = maxSpeed;
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        GameEvents.PowerUp += OnPowerUp;
        GameEvents.PowerDown += OnPowerDown;
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
            runtimeData.fireballOnCD = true;
            animator.SetTrigger("ability01");
        }

        VelocityRefresh();
    }

    protected override void VelocityRefresh()
    {
        base.VelocityRefresh();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {  
        if (other.gameObject.CompareTag("EnemyAttackHitbox") && !runtimeData.hurt)
        {
            GameEvents.health -= 20;
            runtimeData.animationLock = true;
            runtimeData.hurt = true;
            animator.SetBool("hurt", runtimeData.animationLock);
        }

        if (other.gameObject.CompareTag("BossAttackHitbox") && !runtimeData.hurt)
        {
            GameEvents.health -= 40;
            runtimeData.animationLock = true;
            runtimeData.hurt = true;
            animator.SetBool("hurt", runtimeData.animationLock);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StageBounds") && !died)
        {
            print("calling (arg died)");
            runtimeData.winCond = "died";
            runtimeData.gameOver = true;
            died = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if touching an enemy
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Hazard"))
        {
            GameEvents.health -= 10;
            runtimeData.animationLock = true;
            runtimeData.hurt = true;
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

        targetVelocity = move * maxSpeed;
    }

    private void OnPowerUp(object sender, PowerUpEventArgs args)
    {
        if (args.powerUp == "Strength")
        {
            print(animator.Equals(null));
            animator.SetTrigger("strengthUp");
            runtimeData.animationLock = true;
            gameObject.transform.localScale *= 1.5f;
        }

        else if (args.powerUp == "Speed")
        {
            maxSpeed *= 1.5f;
        }
    }

    void OnPowerDown(object sender, EventArgs args)
    {
        gameObject.transform.localScale = baseScale;
        maxSpeed = baseSpeed;
    }

    private void OnDestroy()
    {
        GameEvents.PowerUp -= OnPowerUp;
        GameEvents.PowerDown -= OnPowerDown;
    }
}