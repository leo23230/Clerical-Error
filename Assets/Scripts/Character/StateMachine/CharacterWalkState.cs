using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalkState : CharacterBaseState
{
    private bool isAtAvgDistance;
    private float margin = 0.2f;
    public override void EnterState(CharacterStateManager characterSM)
    {
        Debug.Log("Entered Run State");
        if (characterSM.animator != null)
        {
            Debug.Log("Run Animation");
            characterSM.animator.SetBool("isRunning", true);
        }
        
        characterSM.currentState = characterSM.walkState;
    }

    public override void UpdateState(CharacterStateManager characterSM)
    {
        if (!characterSM.CharacterIsWithinRange())
        {
            //if the character is not within range, we need to lock the if statement
            isAtAvgDistance = false;
        }

        if (!isAtAvgDistance)
        {
            //if(characterIs
            Vector2 targetPosition = new Vector2(characterSM.target.transform.position.x, characterSM.target.transform.position.y);

            Vector2 characterPosition = new Vector2(characterSM.transform.position.x, characterSM.transform.position.y);

            float distanceToTarget = Vector2.Distance(characterPosition, targetPosition);   
            float avgDistance = characterSM.character.avgDistance;
            float upperBound = avgDistance + margin;
            float lowerBound = avgDistance - margin;

            Vector2 newMovePoint = Vector2.MoveTowards(characterSM.transform.position, targetPosition, characterSM.moveSpeed * Time.deltaTime);
            if (distanceToTarget < lowerBound) 
            {
                characterSM.FlipSprite("left");
                newMovePoint = Vector2.MoveTowards(characterSM.transform.position, characterSM.startingPos, characterSM.moveSpeed * Time.deltaTime);
            } 
            else if (distanceToTarget > upperBound)
            {
                characterSM.FlipSprite("right");
                newMovePoint = Vector2.MoveTowards(characterSM.transform.position, targetPosition, characterSM.moveSpeed * Time.deltaTime);
            }

            characterSM.character.characterRigidbody.MovePosition(newMovePoint);

            /*Debug.Log("is within range: " + characterSM.CharacterIsWithinRange());
            Debug.Log("avg dist:" + avgDistance);
            Debug.Log("current dist:" + distanceToTarget);*/

            isAtAvgDistance = distanceToTarget > lowerBound && distanceToTarget < upperBound;

        }
        else
        {
            characterSM.currentState = characterSM.idleState;
        }
        
    }
}
