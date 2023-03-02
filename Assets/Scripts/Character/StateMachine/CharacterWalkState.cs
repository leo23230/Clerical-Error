using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalkState : CharacterBaseState
{
    public override void EnterState(CharacterStateManager characterSM)
    {
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {
        if (!characterSM.CharacterIsWithinRange())
        {
            Vector2 targetPosition = new Vector2(characterSM.target.transform.position.x, characterSM.target.transform.position.y);
            Vector2 newMovePoint = Vector2.MoveTowards(characterSM.transform.position, targetPosition, characterSM.moveSpeed*Time.deltaTime);
            characterSM.character.characterRigidbody.MovePosition(newMovePoint);
        }
        else
        {
            characterSM.currentState = characterSM.idleState;
        }
        
    }
}
