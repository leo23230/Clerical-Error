using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy stats
    //[HideInInspector] public EnemyDetailsSO enemyDetails;
    [HideInInspector] public string enemyName;
    [HideInInspector] public int speed;
    [HideInInspector] public int health = 100;
    //a list of ability ids
    [HideInInspector] public List<string> abilities;

    [HideInInspector] public Health healthComponent;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

    private float damageTimer;
    private float damageTimerSet;
    public int damage;

    private void Awake()
    {
        // Load components
        healthComponent = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //THIS IS TEMPORARY//
        health = 100;
        SetEnemyHealth();

        damageTimerSet = Random.Range(3f, 8f);

        damageTimer = damageTimerSet;
    }

    private void Update()
    {
        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
        }
        else
        {
            GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

            List<GameObject> aliveCharacters = new List<GameObject>();

            for(int i = 0; i < characters.Length; i++)
            {
                var charState = characters[i].GetComponent<CharacterStateManager>();
                if(charState.currentState != charState.deadState)
                {
                    aliveCharacters.Add(characters[i]);
                }
            }

            int randInt = HelperUtilities.RandInt(0f, characters.Length - 1);

            characters[randInt].GetComponent<CharacterStateManager>().DamagePlayer(damage);

            damageTimerSet = Random.Range(3f, 8f);

            damageTimer = damageTimerSet;
        }
    }

    /// <summary>
    /// Initialize the Enemy
    /// </summary>
    public void Initialize(EnemyDetailsSO EnemyDetails)
    {
        /*this.enemyDetails = enemyDetails;
        enemyName = EnemyDetails.enemyName;
        health = EnemyDetails.enemyHealthAmount;
        speed = EnemyDetails.enemySpeed;
        armorClass = EnemyDetails.enemyArmorClass;*/


        // Set Enemy starting health
        SetEnemyHealth();
    }

    /// <summary>
    /// Set Enemy health from EnemyDetails SO
    /// </summary>
    private void SetEnemyHealth()
    {
        healthComponent.SetStartingHealth(health);
    }
}
