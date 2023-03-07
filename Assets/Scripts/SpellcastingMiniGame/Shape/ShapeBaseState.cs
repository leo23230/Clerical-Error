using UnityEngine;

public abstract class ShapeBaseState
{
    public abstract void EnterState(ShapeStateManager shape);

    public abstract void UpdateState(ShapeStateManager shape);

    public abstract void OnMouseOver(ShapeStateManager shape);
}
