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
        //print(patrolTimer);

        if (patrolTimer > patrolForThisTime)
        {
            rb.velocity = Vector2.zero;

            //Set random destination
            float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
            randDir = Random.insideUnitSphere * randRadius;
            randDir.x += transform.position.x;
            randDir.y += transform.position.y;

            patrolTimer = 0f;
        }

        randDir = randDir.normalized;

        if (randDir.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        rb.AddForce(randDir * speed);

        if (Vector2.Distance(transform.position, player.transform.position) <= chaseDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    public void Chase()
    {
        Vector2 dirToPlayer = player.transform.position - transform.position; //get direction between this enemy and player

        if (dirToPlayer.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }  

        dirToPlayer = dirToPlayer.normalized;
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
}
