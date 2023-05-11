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
    private FocusManager focusManager;

    private void Awake()
    {
        focusManager = GameObject.Find("SpellCircle").GetComponent<FocusManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadShapeGroupIntoScrollSnap(shapeGroup1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeShapeGroup(int _num)
    {
        if (_num == 1)
        {
            RemoveCurrentShapesFromScrollSnap(5);
            LoadShapeGroupIntoScrollSnap(shapeGroup1);
        }
        if (_num == 2)
        {
            RemoveCurrentShapesFromScrollSnap(3);
            LoadShapeGroupIntoScrollSnap(shapeGroup2);
        }
    }

    void RemoveCurrentShapesFromScrollSnap(int _num)
    {
        Transform contentTransform = dynamicContentComponent.transform.GetChild(0).Find("Content");
        for(int i = 0; i < _num; i++)
        {
            dynamicContentComponent.Remove(0);
        }
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
