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

    // Use this for initialization
    void Awake()
    {
        runtimeData.attacking = false;
    }

    private void Update()
    {
        //basic attack
        if (Input.GetMouseButtonDown(0))
        {
            runtimeData.attacking = true;
            animator.SetBool("Attack", runtimeData.attacking);
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
        print(runtimeData.attacking);
    }

    protected override void VelocityRefresh()
    {
        base.VelocityRefresh();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //touching weapon pickup allows player to shoot projectiles
        if (other.gameObject.CompareTag("Weapon"))
        {
            hasWeapon = true;
            ammo = 10f;
            Destroy(other.gameObject);
        }

        //touching an enemy results in a game over
        else if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Invoke("RestartLevel", 2f);
        }
    }

    //specifically for falling out of stage to restart level
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StageBounds"))
        {
            gameObject.SetActive(false);
            Invoke("RestartLevel", 2f);
        }
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if (!runtimeData.attacking)
        {
            move.x = Input.GetAxis("Horizontal");
        }

        if (Input.GetButtonDown("Jump") && grounded && !runtimeData.attacking)
        {
            velocity.y = jumpTakeOffSpeed;
        }

        else if (Input.GetButtonUp("Jump") && !runtimeData.attacking)
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