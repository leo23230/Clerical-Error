using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;

public class ShapePopulator : MonoBehaviour
{

    public DynamicContent dynamicContentComponent;
    public List<GameObject> shapeGroup1;
    public List<GameObject> shapeGroup2;
    public List<GameObject> shapeGroup3;

    // Start is called before the first frame update
    void Start()
    {
        LoadShapeGroupIntoScrollSnap(shapeGroup1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadShapeGroupIntoScrollSnap(List<GameObject> _shapes)
    {
        foreach(GameObject shape in _shapes)
        {
            dynamicContentComponent.panelPrefab = shape;
            dynamicContentComponent.AddAtIndex();
        }
    }
}
