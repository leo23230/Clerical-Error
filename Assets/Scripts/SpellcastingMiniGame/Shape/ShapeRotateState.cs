using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRotateState : ShapeBaseState
{
    public override void EnterState(ShapeStateManager shape)
    {
        shape.updatePosition(0f, 0f);
        shape.updateSortingLayer(shape.rotatingShapeLayer);
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
