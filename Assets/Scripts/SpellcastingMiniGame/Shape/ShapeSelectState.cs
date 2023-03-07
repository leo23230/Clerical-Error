using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSelectState : ShapeBaseState
{
    public override void EnterState(ShapeStateManager shape)
    {
        shape.updatePosition(shape.shapeStartingX, shape.shapeStartingY);
        shape.updateSortingLayer(shape.firstLayer);
    }

    public override void UpdateState(ShapeStateManager shape)
    {

    }

    public override void OnMouseOver(ShapeStateManager shape)
    {
        if (Input.GetMouseButtonDown(0))
        {
            shape.shapeRotateState.EnterState(shape);
            shape.currentState = shape.shapeRotateState;
        }
    }
}
