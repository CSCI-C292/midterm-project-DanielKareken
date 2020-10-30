using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadlylion : Enemy
{
    private void Start()
    {
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
        if (Vector2.Distance(transform.position, player.transform.position) <= chaseDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    //this class enemy does NOT move
    public void Chase()
    {
        Vector2 dirToPlayer = player.transform.position - transform.position; //get direction between this enemy and player

        if (dirToPlayer.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= aggroRange)
        {
            enemyState = EnemyState.ATTACK;
        }

        //if player runs away from enemy
        else if (Vector2.Distance(transform.position, player.transform.position) > chaseDistance)
        {
            enemyState = EnemyState.PATROL;
        }
    }
}
