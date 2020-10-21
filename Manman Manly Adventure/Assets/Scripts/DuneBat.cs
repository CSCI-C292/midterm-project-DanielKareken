using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuneBat : Enemy
{
    float currentChaseDistance;
    float patrolTimer;
    Vector2 randDir;

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
        patrolTimer += Time.deltaTime;
        print(patrolTimer);
        if (patrolTimer > patrolForThisTime)
        {
            //Set random destination
            float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
            randDir = Random.insideUnitSphere * randRadius;
            randDir += (Vector2)transform.position;

            patrolTimer = 0f;
        }

        randDir = randDir.normalized;
        print("Test: " + randDir); 
        rb.AddForce(randDir * speed);

        if (Vector3.Distance(transform.position, player.transform.position) <= chaseDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    public void Chase()
    {
        Vector2 dirToPlayer = player.transform.position - transform.position; //get direction between this enemy and player

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
