using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempshutoff : MonoBehaviour
{
    public GameObject backPackMiniGame;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            backPackMiniGame.SetActive(!backPackMiniGame.activeSelf);
        }
    }
}
