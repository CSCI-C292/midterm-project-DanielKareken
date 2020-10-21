using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadlylion : Enemy
{
    float attackTimer;

    private void Start()
    {
        enemyState = EnemyState.PATROL;
        attackTimer = waitBeforeAttack;
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
        attackTimer = waitBeforeAttack - 0.1f;

        if (dirToPlayer.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
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

    new public void Attack()
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
        }
    }
}
