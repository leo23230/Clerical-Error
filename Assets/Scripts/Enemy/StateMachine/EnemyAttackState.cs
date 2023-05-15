using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{

    float attackReadyCoolDown = 0f;

    string attackAnimationBoolName;

    public override void EnterState(EnemyStateManager enemySM)
    {
        if (enemySM.transform.position.x < enemySM.target.transform.position.x)
        {
            enemySM.FlipSprite("left");
        }
        else
        {
            enemySM.FlipSprite("right");
        }

        enemySM.animator.SetBool("isMoving", false);

        attackAnimationBoolName = enemySM.enemy.enemyDetails.enemyAttackAnimationBoolName;

        //this function sets animation automatically
        setAttackCoolDownTime(enemySM);

        enemySM.currentState = enemySM.attackState;
    }

    public override void UpdateState(EnemyStateManager enemySM)
    {
        bool isTargetToLeft = enemySM.transform.position.x < enemySM.target.transform.position.x;
        if (isTargetToLeft)
        {
            enemySM.FlipSprite("left");
        }
        else
        {
            enemySM.FlipSprite("right");
        }

        if (enemySM.target.GetComponent<Health>().GetHealth() <= 0)
        {
            if (enemySM.FindAliveCharacters().Count > 0 && enemySM.PlayerIsAlive())
            {
                enemySM.target = enemySM.SelectCharacter();
            }
            else
            {
                enemySM.idleState.EnterState(enemySM);
            }
        }

        if (enemySM.EnemyIsWithinRange())
        {
            if (attackReadyCoolDown <= 0f)
            {
                enemySM.animator.SetBool(attackAnimationBoolName, true);
                enemySM.target.GetComponent<CharacterStateManager>().DamagePlayer(enemySM.enemy.damage);
                setAttackCoolDownTime(enemySM);
            }
            if (attackReadyCoolDown > 0f)
            {
                attackReadyCoolDown -= Time.deltaTime;
            }
        }
        else
        {
            enemySM.moveState.EnterState(enemySM);
        }
    }

    private void setAttackCoolDownTime(EnemyStateManager enemySM)
    {
        attackReadyCoolDown = enemySM.enemy.attackCooldown;
    }

}
