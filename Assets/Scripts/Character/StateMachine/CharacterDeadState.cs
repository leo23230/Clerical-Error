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
        }

        characterSM.InstantiateEffectPrefab(characterSM.deathEffect);

        characterSM.character.sortingGroup.sortingLayerName = "Dead";

        characterSM.currentState = characterSM.deadState;
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {

    }
}

