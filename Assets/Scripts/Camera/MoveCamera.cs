using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [HideInInspector] public bool moveCameraPosition = false;
    [HideInInspector] public float targetX;
    [HideInInspector] public float targetY;
    private void Awake()
    {
        targetX = transform.position.x;
        targetY = transform.position.y;
    }

    private void OnEnable()
    {
        StaticEventHandler.BagRummageEvent += moveCameraToBagRummage;
        StaticEventHandler.ExitMiniGameEvent += moveCameraBack;
    }
    private void OnDisable()
    {
        StaticEventHandler.BagRummageEvent -= moveCameraToBagRummage;
        StaticEventHandler.ExitMiniGameEvent -= moveCameraBack;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 _target = new Vector3(targetX, targetY, 0f);
        moveCameraPosition = !(transform.position.Equals(_target));
        if (moveCameraPosition)
        {
            transform.position = Vector3.Lerp(transform.position, _target, 0.9f);
            if (transform.position.y > targetY * .95 && transform.position.x > targetX * .95) 
            {
                transform.position = _target;
            }
        }
    }

    public void moveCameraToBagRummage(BagRummageEventArgs bagRummageEventArgs)
    {
        targetY = 16.875f;
    }

    public void moveCameraBack(ExitMiniGameEventArgs exitMiniGameEventArgs)
    {
        targetY = 0.0f;
    }

}
