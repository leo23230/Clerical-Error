using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy stats
    //[HideInInspector] public EnemyDetailsSO enemyDetails;
    [HideInInspector] public string enemyName;
    [HideInInspector] public int speed;
    [HideInInspector] public int health;
    //a list of ability ids
    [HideInInspector] public List<string> abilities;

    [HideInInspector] public Health healthComponent;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        // Load components
        healthComponent = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
