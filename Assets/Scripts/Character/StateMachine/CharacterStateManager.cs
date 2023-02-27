using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    [HideInInspector] public Character character;
    [HideInInspector] public CharacterBaseState currentState;

    private void Awake()
    {
        //the character component is repsonible for storing
        //the character stats from the scriptable object, and 
        //relevant component data that the state manager will use
        character = gameObject.GetComponent<Character>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        //keep track of all ability cooldowns
    }
}
