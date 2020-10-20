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
    
    bool facingRight = false;

    //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] RuntimeData runtimeData;

    //public GameObject projectilePrefab;
    //public Transform shotSpawn;
    public GameObject swordHitbox;

    // Use this for initialization
    void Awake()
    {
        runtimeData.animationLock = false;
        GameEvents.health = 100;
        swordHitbox.SetActive(false);
    }

    private void Update()
    {
        //basic attack
        if (Input.GetMouseButtonDown(0) && runtimeData.animationLock == false)
        {
            runtimeData.animationLock = true;
            animator.SetTrigger("Attack");
            swordHitbox.SetActive(true);
        }

        //ability1 attack
        if (Input.GetMouseButtonDown(1) && runtimeData.animationLock == false)
        {
            runtimeData.animationLock = true;
            animator.SetTrigger("ability01");
            swordHitbox.SetActive(true);
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
            gameObject.SetActive(false);
            Invoke("RestartLevel", 2f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if touching an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameEvents.health -= 10;
            runtimeData.animationLock = true;
            animator.SetBool("hurt", runtimeData.animationLock);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        //if touching an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            runtimeData.animationLock = false;
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
            if (facingRight == false)
            {
                facingRight = true;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
        else if (move.x < -0.01f)
        {
            if (facingRight == true)
            {
                facingRight = false;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    //reloads level after player dies or all collectables are picked up
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}