using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStateManager : MonoBehaviour
{

    [HideInInspector] public SpriteRenderer spriteRenderer;


    //info
    public float shapeStartingX;
    public float shapeStartingY;
    [HideInInspector] public float shapeScale;
    [HideInInspector] public string firstLayer = "SBfirst";
    [HideInInspector] public string rotatingShapeLayer = "SBRotatingShape";


    //state machine stuff
    [HideInInspector] public ShapeBaseState currentState;
    [HideInInspector] public ShapeSelectState shapeSelectState = new ShapeSelectState();
    [HideInInspector] public ShapeRotateState shapeRotateState = new ShapeRotateState();
    [HideInInspector] public ShapeConfirmedState shapeConfirmedState = new ShapeConfirmedState();

    // Start is called before the first frame update

    private void Awake()
    {
        //cache
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        currentState = shapeSelectState;
    }

    private void Start()
    {
        //call enter state function first
        currentState.EnterState(this);
    }
    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnMouseOver()
    {
        currentState.OnMouseOver(this);
    }

    public void updatePosition(float _x, float _y)
    {
        var newPos = new Vector3(_x, _y, transform.position.z);
        transform.position = newPos;
    }

    public void updateSortingLayer(string _layerName)
    {
        spriteRenderer.sortingLayerName = _layerName;
    }
}
