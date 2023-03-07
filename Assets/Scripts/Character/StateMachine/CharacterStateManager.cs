using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStateManager : MonoBehaviour
{

    //state manager stuff//
    [HideInInspector] public CharacterIdleState idleState = new CharacterIdleState();
    [HideInInspector] public CharacterWalkState walkState = new CharacterWalkState();
    [HideInInspector] public CharacterAttackState attackState = new CharacterAttackState();

    //stats//
    [HideInInspector] public Character character;
    [HideInInspector] public CharacterBaseState currentState;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float abilityReadyCooldown;

    //components//
    [HideInInspector] public Animator animator;

    //other//
    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject[] enemies;
    [HideInInspector] public List<Ability> abilities = new List<Ability>();
    private void Awake()
    {
        //the character component is repsonible for storing
        //the character stats from the scriptable object, and 
        //relevant component data that the state manager will use

        //set character stats
        character = gameObject.GetComponent<Character>();
        

        target = SelectEnemy();

    }
    void Start()
    {
        //stat cahce//
        animator = character.animator;
        moveSpeed = character.speed;
        abilityReadyCooldown = character.abilityReadyCooldown;

        InstantiateAbilities();

        idleState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);

        //always need to update cool down timers
        UpdateCoolDownTimers();
    }

    GameObject SelectEnemy() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var target = enemies[Mathf.RoundToInt(UnityEngine.Random.Range(0f, enemies.Length))];
        return target;
    }

    public bool CharacterIsWithinRange()
    {
        if(target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            return distanceToTarget <= character.minRange;
        }
        else
        {
            return true;
        }
      
    }

    //this function will take the list of attacks and based on current ability statuses / game state determine which ability to use
    public bool ChooseAnAbility()
    {
        //collect a list of ready abilities
        List<Ability> readyAbilities = new List<Ability>();

        foreach (Ability ability in abilities)
        {
            if (ability.AbilityIsReady()) readyAbilities.Add(ability);
        }

        if (readyAbilities.Count <= 0) 
        {
            return false;
        } 
        else if (readyAbilities.Count == 1)
        {
            Ability chosenAbility = readyAbilities[0];
            chosenAbility.useAbility(target);
            Debug.Log("Player used " + chosenAbility.name);

            //start animation
            if(animator != null) animator.SetBool("isAttacking", true);

            return true;
        }
        else
        {
            int random = UnityEngine.Random.Range(0, readyAbilities.Count);
            Ability chosenAbility = readyAbilities[random];
            chosenAbility.useAbility(target);
            Debug.Log(character.name + " used " + chosenAbility.name);

            //start animation
            animator.SetBool("isAttacking", true);

            return true;
        }
    }

    public void InstantiateAbilities()
    {
        foreach (String abilityName in character.abilities)
        {
            //Finds the type that matches the ability name
            System.Type abilityType = Type.GetType(abilityName);

            //creates an instance of the ability (type cast as Ability so methods can be used)
            Ability abilityInstance = (Ability) Activator.CreateInstance(abilityType);

            //add the ability to the list
            abilities.Add(abilityInstance);

        }
    }

    public void UpdateCoolDownTimers()
    {
        foreach (Ability ability in abilities)
        {
            ability.updateCoolDownTimer();
        }
    }
}
