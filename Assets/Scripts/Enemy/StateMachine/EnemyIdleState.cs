using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemySM)
    {
        //Debug.Log("Entered Idle State");

        enemySM.currentState = enemySM.idleState;
    }

    public override void UpdateState(EnemyStateManager enemySM)
    {
        if (enemySM.EnemyIsWithinRange())
        {
            //determine which attack to use based on cooldowns and other stuff
            //then send the enemy into the attack state

           // Debug.Log("WithinRange");

            enemySM.attackState.EnterState(enemySM);
        }
        else
        {
            enemySM.moveState.EnterState(enemySM);
        }
    }
}
