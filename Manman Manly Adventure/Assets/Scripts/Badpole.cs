﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badpole : Enemy
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

    public void Patrol()
    {
        anim.SetBool("moving", false);
        patrolTimer += Time.deltaTime;

        if (patrolTimer > patrolForThisTime)
        {
            //Set random destination
            float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
            randDir = Random.insideUnitSphere * randRadius;
            randDir.x += transform.position.x;
            randDir.y += transform.position.y;

            patrolTimer = 0f;
        }

        //check if enemy moving
        if (rb.velocity.x != 0f)
        {
            anim.SetBool("moving", true);
        }

        else
        {
            anim.SetBool("moving", false);
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
        anim.SetBool("moving", true);

        if (dirToPlayer.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        dirToPlayer = dirToPlayer.normalized;
        //dirToPlayer.y = 0f;
        rb.AddForce(dirToPlayer * speed); //moves enemy

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
