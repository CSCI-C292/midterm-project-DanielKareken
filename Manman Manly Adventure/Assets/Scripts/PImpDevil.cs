using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PImpDevil : Enemy
{
    float currentChaseDistance;
    float patrolTimer;
    Vector2 randDir;
    float health;

    private float devilAttackTimer;

    private void Start()
    {
        currentChaseDistance = chaseDistance;
        patrolTimer = patrolForThisTime;

        enemyState = EnemyState.PATROL;
        devilAttackTimer = waitBeforeAttack;

        health = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyState == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }

        if (enemyState == EnemyState.ATTACK)
        {
            Attack();
        }        
    }

    public new void OnTriggerEnter2D(Collider2D collision)
    {
        //if successfully hit by player attack
        if (collision.gameObject.CompareTag("PlayerAttackHitbox") )
        {
            health -= 20;
        }

        if (health <= 0)
        {
            anim.SetTrigger("hurt");
            GameEvents.score += 1000;
        }
    }

    public void Patrol()
    {
        anim.SetBool("moving", false);

        if (Vector2.Distance(transform.position, player.transform.position) <= chaseDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    public void Chase()
    {
        Vector2 dirToPlayer = player.transform.position - transform.position; //get direction between this enemy and player
        anim.SetBool("moving", true);

        if (dirToPlayer.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        dirToPlayer = dirToPlayer.normalized;
        dirToPlayer.y = 0;
        rb.AddForce(dirToPlayer * speed * 2); //moves enemy

        if (Vector2.Distance(transform.position, player.transform.position) <= aggroRange)
        {
            enemyState = EnemyState.ATTACK;
            rb.velocity = Vector2.zero;
            //play audio?

            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }

        //if player runs away from enemy
        else if (Vector2.Distance(transform.position, player.transform.position) > chaseDistance)
        {
            enemyState = EnemyState.PATROL;

            patrolTimer = patrolForThisTime;

            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
    }

    new public void Attack()
    {
        devilAttackTimer += Time.deltaTime;
        anim.SetBool("moving", false);

        if (devilAttackTimer > waitBeforeAttack)
        {
            float chooseAttack = UnityEngine.Random.Range(0f, 3f);

            if (chooseAttack < 1f)
            {
                anim.SetTrigger("ClawAttack");           
            }
            else if (chooseAttack < 2f)
            {
                anim.SetTrigger("PitchforkAttack");
            }
            else
            {
                anim.SetTrigger("SwordAttack");
            }

            devilAttackTimer = 0f; 
        }

        if (Vector2.Distance(transform.position, player.transform.position) > aggroRange + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.CHASE;
            devilAttackTimer = waitBeforeAttack - 0.1f;
        }
    }
}
