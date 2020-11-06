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
    [SerializeField] RuntimeData runtimeData;

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
    public GameObject attackHitbox;

    public AudioSource attackSound;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        attackTimer = waitBeforeAttack;
        attackHitbox.SetActive(false);
        attackSound = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer > waitBeforeAttack)
        {
            anim.SetTrigger("attack");
            attackTimer = 0f;
        }

        if (Vector2.Distance(transform.position, player.transform.position) > aggroRange + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.CHASE;
            attackTimer = waitBeforeAttack - 0.1f;
        }
    }
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if successfully hit by player attack
        if (collision.gameObject.CompareTag("PlayerAttackHitbox"))
        {
            GameEvents.score += 100;
            runtimeData.pacifist = false;
            anim.SetTrigger("hurt");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void EnableAttackHitbox()
    {
        attackHitbox.SetActive(true);
    }

    public void DisableAttackHitbox()
    {
        attackHitbox.SetActive(false);
    }

    public void playAttackSound()
    {
        attackSound.Play();
    }
}
