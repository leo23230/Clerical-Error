using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private CircleCollider2D col;
    private void Awake()
    {
        col = gameObject.GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("poop");
            col.isTrigger = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            col.isTrigger = true;
        }
    }
}
