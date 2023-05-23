using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    private bool isAtAvgDistance;
    private float margin = 0.3f;
    public override void EnterState(EnemyStateManager enemySM)
    {
        Debug.Log("Entered Move State");
        enemySM.animator.SetBool("isMoving", true);

        enemySM.currentState = enemySM.moveState;
    }

    public override void UpdateState(EnemyStateManager enemySM)
    {

        if (!enemySM.EnemyIsWithinRange())
        {
            //if the enemy is not within range, we need to lock the if statement
            isAtAvgDistance = false;
        }

        if (!isAtAvgDistance)
        {
            //if(enemyIs
            Vector2 targetPosition = new Vector2(enemySM.target.transform.position.x, enemySM.target.transform.position.y);

            Vector2 enemyPosition = new Vector2(enemySM.transform.position.x, enemySM.transform.position.y);

            float distanceToTarget = Vector2.Distance(enemyPosition, targetPosition);
            float avgDistance = enemySM.enemy.avgDistance;
            float upperBound = avgDistance + margin;
            float lowerBound = avgDistance - margin;

            Vector2 newMovePoint = Vector2.MoveTowards(enemySM.transform.position, targetPosition, enemySM.moveSpeed * Time.deltaTime);

            //if the target is too close
            if (distanceToTarget < lowerBound)
            {
                Debug.Log("moving away from target");
                enemySM.FlipSprite("left");
                newMovePoint = Vector2.MoveTowards(enemySM.transform.position, enemySM.startingPos, enemySM.moveSpeed * Time.deltaTime);
            }
            //if the target is too far away
            else if (distanceToTarget > upperBound)
            {
                Debug.Log("Moving towards target");
                enemySM.FlipSprite("right");
                newMovePoint = Vector2.MoveTowards(enemySM.transform.position, targetPosition, enemySM.moveSpeed * Time.deltaTime);
            }

            enemySM.enemy.rigidBody.MovePosition(newMovePoint);

            /*Debug.Log("is within range: " + enemySM.CharacterIsWithinRange());
            Debug.Log("avg dist:" + avgDistance);
            Debug.Log("current dist:" + distanceToTarget);*/

            isAtAvgDistance = distanceToTarget > lowerBound && distanceToTarget < upperBound;

        }
        else
        {
            enemySM.idleState.EnterState(enemySM);
        }
    }

}
