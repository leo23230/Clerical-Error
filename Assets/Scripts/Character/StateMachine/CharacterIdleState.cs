using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterBaseState
{
    public override void EnterState(CharacterStateManager characterSM)
    {
        Debug.Log("Entered Idle State");
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {
        if (characterSM.CharacterIsWithinRange())
        {
            //determine which attack to use based on cooldowns and other stuff
            //then send the character into the attack state

            characterSM.attackState.EnterState(characterSM);
            characterSM.currentState = characterSM.attackState;
        }
        else
        {
            characterSM.currentState = characterSM.walkState;
        }
    }
}
