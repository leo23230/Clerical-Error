using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadState : CharacterBaseState
{
    public override void EnterState(CharacterStateManager characterSM)
    {
        if (characterSM.animator != null)
        {
            characterSM.animator.SetBool("isRunning", false);
            characterSM.animator.SetBool("isReadying", false);
            characterSM.animator.SetBool("isAttacking", false);
            characterSM.animator.SetBool("isDead", true);
        }

        characterSM.InstantiateEffectPrefab(characterSM.deathEffect);

        //characterSM.character.sortingGroup.sortingLayerName = "Dead";

        characterSM.currentState = characterSM.deadState;
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {
        //if(characterSM.character.sortingGroup.sortingLayerName != "Dead")characterSM.character.sortingGroup.sortingLayerName = "Dead";
    }
}

