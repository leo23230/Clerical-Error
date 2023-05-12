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

        if (characterSM.animator != null) characterSM.animator.SetBool("isRunning", false);

        //this function sets animation automatically
        if (characterSM.ChooseAnAbility()) setAttackCoolDownTime(characterSM);

        characterSM.currentState = characterSM.attackState;
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {
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
            if (!characterSM.character.animator.GetBool("isReadying"))
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
