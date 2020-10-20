using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    private SpriteRenderer spriteRenderer;
    Rigidbody2D rb; //refernce to self rigidbody
    public float speed;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Follow player if player in range
        Vector2 dirToPlayer = player.transform.position - transform.position; //get direction between this enemy and player

        if (dirToPlayer.x <= 10f && dirToPlayer.x >= -10f)
        {
            if (dirToPlayer.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            dirToPlayer = dirToPlayer.normalized;
            rb.AddForce(dirToPlayer * speed); //moves enemy
        }

        //if player not close enough, standard patrol pathing
        patrol();
    }

    void patrol()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if successfully hit by player attack
        if (collision.gameObject.CompareTag("PlayerAttackHitbox"))
        {
            GameEvents.score += 100;
            Destroy(gameObject);
        }
    }
}
