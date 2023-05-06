using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : CharacterBaseState
{
    public override void EnterState(CharacterStateManager characterSM)
    {
        characterSM.currentState = characterSM.idleState;

        if(!characterSM.characterCanvas.activeSelf) characterSM.characterCanvas.SetActive(true);
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {
        if (characterSM.CharacterIsWithinRange())
        {
            //determine which attack to use based on cooldowns and other stuff
            //then send the character into the attack state

            if (characterSM.findAliveEnemies().Count > 0) characterSM.attackState.EnterState(characterSM);
        }
        else
        {
            characterSM.walkState.EnterState(characterSM);
        }
    }
}
