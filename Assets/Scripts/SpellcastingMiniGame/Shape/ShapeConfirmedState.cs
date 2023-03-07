using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeConfirmedState : ShapeBaseState
{
    public override void EnterState(ShapeStateManager shape)
    {
    }

    public override void UpdateState(ShapeStateManager shape)
    {
    }

    public override void OnMouseOver(ShapeStateManager shape)
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
}
