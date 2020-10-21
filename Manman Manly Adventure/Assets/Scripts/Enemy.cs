using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class Enemy : MonoBehaviour
{
    public float speed;
    public float aggroRange;
    public EnemyState enemyState;

    public GameObject player;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb; //refernce to self rigidbody
    public Animator anim;

    public float chaseAfterAttackDistance;
    public float chaseDistance;

    public float patrolRadiusMin, patrolRadiusMax;
    public float patrolForThisTime;

    public float waitBeforeAttack;
    private float attackTimer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        attackTimer = waitBeforeAttack;
    }

    public void Attack()
    {
        rb.velocity = Vector2.zero;
        attackTimer += Time.deltaTime;

        if (attackTimer > waitBeforeAttack)
        {
            anim.SetTrigger("attack");
            attackTimer = 0f;
        }

        if (Vector2.Distance(transform.position, player.transform.position) > aggroRange + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.CHASE;
        }
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
