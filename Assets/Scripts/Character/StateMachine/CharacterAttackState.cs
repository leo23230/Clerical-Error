using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
    float attackCoolDown = 0f;
    float attackCoolDownTime = 1f; 
    public override void EnterState(CharacterStateManager characterSM)
    {
        if(characterSM.ChooseAnAbility()) setAttackCoolDownTime();
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {
        if(attackCoolDown <= 0f)
        {
            //if an ability is actually chosen, then we'll set the cool down
            if(characterSM.ChooseAnAbility()) setAttackCoolDownTime();
        }
        if(attackCoolDown > 0f) attackCoolDown -= Time.deltaTime;
    }

    private void setAttackCoolDownTime()
    {
        attackCoolDown = attackCoolDownTime;
    }
}
