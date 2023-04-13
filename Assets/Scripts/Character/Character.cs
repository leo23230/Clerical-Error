using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
#endregion REQUIRE COMPONENTS

public class Character : MonoBehaviour
{
    //character stats
    private int wow;
    [HideInInspector] public CharacterDetailsSO characterDetails;
    [HideInInspector] public string characterName;
    [HideInInspector] public float speed;
    [HideInInspector] public int health;
    [HideInInspector] public float armorClass;
    [HideInInspector] public float minDistance;
    [HideInInspector] public float maxDistance;
    [HideInInspector] public float avgDistance;
    [HideInInspector] public float abilityReadyCooldown;
    //a list of ability ids
    [HideInInspector] public List<string> abilities;

    [HideInInspector] public Health healthComponent;
    [HideInInspector] public CooldownBarManager CDBarManager;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public GameObject sprite;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D characterRigidbody;
    [HideInInspector] public SortingGroup sortingGroup;

    //attached
    public GameObject alertPrefab;
    [HideInInspector] public GameObject alertObject;
    [HideInInspector] public AlertMonobehaviour alertMonobehaviour;


    private void Awake()
    {
        // Load components
        healthComponent = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        CDBarManager = GetComponent<CooldownBarManager>();       
        characterRigidbody = GetComponent<Rigidbody2D>();
        sortingGroup = GetComponent<SortingGroup>();
        sprite = transform.Find("Sprite").gameObject;
        //animator = sprite.GetComponent<Animator>();
        if (sprite != null) animator = sprite.GetComponent<Animator>();

        //Alert//
        Transform alertCanvasTransform = GameObject.Find("AlertCanvas").transform;

        alertObject = Instantiate(alertPrefab, alertCanvasTransform);
        
        alertMonobehaviour = alertObject.GetComponent<AlertMonobehaviour>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        alertMonobehaviour.UpdatePosition(transform.position.x);
    }

    /// <summary>
    /// Initialize the character
    /// </summary>
    public void Initialize(CharacterDetailsSO characterDetails)
    {
        this.characterDetails = characterDetails;
        characterName = characterDetails.characterName;
        health = characterDetails.characterHealthAmount;
        speed = characterDetails.characterSpeed;
        armorClass = characterDetails.characterArmorClass;
        maxDistance = characterDetails.characterAttackMax;
        minDistance = characterDetails.characterAttackMin;
        avgDistance = minDistance + ((maxDistance - minDistance) /2);
        abilities = characterDetails.characterAbilities;
        abilityReadyCooldown = characterDetails.characterAbilityCooldown;


        // Set character starting health
        SetCharacterHealth();
    }

    /// <summary>
    /// Set character health from characterDetails SO
    /// </summary>
    private void SetCharacterHealth()
    {
        healthComponent.SetStartingHealth(health);
    }
}