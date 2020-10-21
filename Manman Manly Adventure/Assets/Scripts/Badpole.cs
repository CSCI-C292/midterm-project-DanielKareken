using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badpole : Enemy
{
    float currentChaseDistance;
    float patrolTimer;

    private void Start()
    {
        currentChaseDistance = chaseDistance;
        patrolTimer = patrolForThisTime;

        enemyState = EnemyState.PATROL;
    }

    // Update is called once per frame
    void Update()
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

    public void Patrol()
    {
        anim.SetBool("moving", false);
        patrolTimer += Time.deltaTime;

        if (patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }

        //check if enemy moving
        /*if (moving)
        {
            anim.SetBool("moving", true);
        }

        else
        {
            anim.SetBool("moving", false);
        }*/

        if (Vector3.Distance(transform.position, player.transform.position) <= chaseDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    //helper for Patrol()
    void SetNewRandomDestination()
    {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector2 randDir = Random.insideUnitCircle * randRadius;
        randDir += (Vector2)transform.position;
    }

    public void Chase()
    {
        Vector2 dirToPlayer = player.transform.position - transform.position; //get direction between this enemy and player
        anim.SetBool("moving", true);

        if (dirToPlayer.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        else
        {
            spriteRenderer.flipX = false;
        }

        dirToPlayer = dirToPlayer.normalized;
        rb.AddForce(dirToPlayer * speed * 2); //moves enemy

        if (Vector2.Distance(transform.position, player.transform.position) <= aggroRange)
        {
            enemyState = EnemyState.ATTACK;

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

}
