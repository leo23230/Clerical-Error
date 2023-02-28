using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{

    //state manager stuff//
    [HideInInspector] public CharacterIdleState idleState = new CharacterIdleState();
    [HideInInspector] public CharacterWalkState walkState = new CharacterWalkState();
    [HideInInspector] public CharacterAttackState attackState = new CharacterAttackState();

    //stats//
    [HideInInspector] public Character character;
    [HideInInspector] public CharacterBaseState currentState;
    public float moveSpeed = 2f;

    //other//
    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject[] enemies;

    private void Awake()
    {
        //the character component is repsonible for storing
        //the character stats from the scriptable object, and 
        //relevant component data that the state manager will use
        character = gameObject.GetComponent<Character>();
        target = SelectEnemy();
        currentState = walkState;


    }
    void Start()
    {
        
    }
    void Update()
    {
        currentState.UpdateState(this);
    }

    GameObject SelectEnemy() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var target = enemies[Mathf.RoundToInt(Random.Range(0f, enemies.Length))];
        return target;
    }
}
