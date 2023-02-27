using UnityEngine;

public abstract class CharacterBaseState
{
    public abstract void EnterState(CharacterStateManager Character /*context*/);

    public abstract void UpdateState(CharacterStateManager Character);
}
