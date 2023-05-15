using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
    float attackReadyCoolDown = 0f;
    public override void EnterState(CharacterStateManager characterSM)
    {
        if(characterSM.transform.position.x < characterSM.target.transform.position.x)
        {
            characterSM.FlipSprite("right");
        }
        else
        {
            characterSM.FlipSprite("left");
        }

        if (characterSM.isInAltState)
        {
            characterSM.animator.SetBool("isRunningAlt", false);
        }
        else
        {
            //incase it is running out of alt mode
            characterSM.animator.SetBool("isRunningAlt", false);
            characterSM.animator.SetBool("isRunning", false);
        }

        //this function sets animation automatically
        if (characterSM.ChooseAnAbility()) setAttackCoolDownTime(characterSM);

        characterSM.currentState = characterSM.attackState;
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {
        Debug.Log(characterSM.character.name + " is Attacking");
        if (characterSM.target.GetComponent<Health>().GetHealth() <= 0)
        {
            if(characterSM.findAliveEnemies().Count > 0) 
            {
                characterSM.target = characterSM.SelectEnemy();
            }
            else
            {
                characterSM.idleState.EnterState(characterSM);
            }
        }

        if (characterSM.CharacterIsWithinRange())
        {
            if (attackReadyCoolDown <= 0f)
            {
                //if an ability is actually chosen, then we'll set the cool down
                if (characterSM.ChooseAnAbility())
                {
                    setAttackCoolDownTime(characterSM);
                }
            }
            if (attackReadyCoolDown > 0f) 
            {
                attackReadyCoolDown -= Time.deltaTime;
            } 
        }
        else
        {
            //wait for all of these animations to finish
            if (!characterSM.character.animator.GetBool("isReadying")&&
                !characterSM.character.animator.GetBool("isSwitching") &&
                !characterSM.character.animator.GetBool("isReturning") &&
                !characterSM.character.animator.GetBool("isAttacking"))
            {
                if (characterSM.character.hasBackupAbility)
                {
                    //Since we're not using normal abilities, we ignore the base cooldown
                    //if an ability is actually chosen, then we'll set the cool down
                    if (characterSM.UseBackupAbility())
                    {
                        setAttackCoolDownTime(characterSM);
                    }
                }
                else
                {
                    characterSM.walkState.EnterState(characterSM);
                }       
            }
        }
        
    }

    private void setAttackCoolDownTime(CharacterStateManager characterSM)
    {
        attackReadyCoolDown = characterSM.abilityReadyCooldown;
    }


}
