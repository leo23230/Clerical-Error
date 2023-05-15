using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy stats
    //[HideInInspector] public EnemyDetailsSO enemyDetails;
    [HideInInspector] public EnemyDetailsSO enemyDetails;
    [HideInInspector] public string enemyName;
    [HideInInspector] public float speed;
    [HideInInspector] public float armorClass;
    [HideInInspector] public int health = 100;
    [HideInInspector] public float attackDamage;
    [HideInInspector] public float attackCooldown;
    [HideInInspector] public float minDistance;
    [HideInInspector] public float maxDistance;
    [HideInInspector] public float avgDistance;

    [HideInInspector] public bool dead;

    //a list of ability ids
    [HideInInspector] public List<string> abilities;

    [HideInInspector] public Health healthComponent;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public GameObject sprite;
    [HideInInspector] public GameObject redFlashLight;

    public Animator animator;


    private float damageTimer;
    private float damageTimerSet;
    public int damage;

    private void Awake()
    {
        // Load components
        healthComponent = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = transform.Find("Sprite").gameObject;
        rigidBody = GetComponent<Rigidbody2D>();

        damageTimerSet = Random.Range(3f, 8f);

        damageTimer = damageTimerSet;

        redFlashLight = transform.Find("RedFlash").gameObject;
        redFlashLight.SetActive(false);

        dead = false;
    }

    private void Update()
    {
        if (!dead)
        {
            //AttackCharacters();
        }

        if (healthComponent.GetHealth() <= 0) dead = true;
    }

    /// <summary>
    /// Initialize the Enemy
    /// </summary>
    public void Initialize(EnemyDetailsSO enemyDetails)
    {
        this.enemyDetails = enemyDetails;
        enemyName = enemyDetails.enemyName;
        health = enemyDetails.enemyHealthAmount;
        speed = enemyDetails.enemySpeed;
        armorClass = enemyDetails.enemyArmorClass;
        attackDamage = enemyDetails.enemyDamage;
        attackCooldown = enemyDetails.enemyAbilityCooldown;
        maxDistance = enemyDetails.enemyAttackMax;
        minDistance = enemyDetails.enemyAttackMin;
        avgDistance = minDistance + ((maxDistance - minDistance) / 2);

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

    public void TakeDamage(int _amt)
    {
        healthComponent.SubtractHealth(_amt);
        StartCoroutine(RedFlash());

        if(healthComponent.GetHealth() <= 0)
        {
            //LevelSequencer.Instance.UpdateEnemyList(gameObject);
            StaticEventHandler.CallEnemyDiedEvent();
            Destroy(gameObject);
        }
    }

    public IEnumerator RedFlash()
    {
        redFlashLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        redFlashLight.SetActive(false);
    }

    private void AttackCharacters()
    {
        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
        }
        else
        {
            GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

            List<GameObject> aliveCharacters = new List<GameObject>();

            for (int i = 0; i < characters.Length; i++)
            {
                var charState = characters[i].GetComponent<CharacterStateManager>();
                if (charState.currentState != charState.deadState)
                {
                    aliveCharacters.Add(characters[i]);
                }
            }

            int randInt = HelperUtilities.RandInt(0f, characters.Length - 1);

            if(aliveCharacters.Count > 0)
            {
                aliveCharacters[randInt].GetComponent<CharacterStateManager>().DamagePlayer(damage);

                damageTimerSet = Random.Range(3f, 8f);

                damageTimer = damageTimerSet;
            }
            
        }
    }
}
