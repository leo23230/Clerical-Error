using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy /*context*/);

    public abstract void UpdateState(EnemyStateManager enemy);
}
