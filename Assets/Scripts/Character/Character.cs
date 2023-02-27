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
    [HideInInspector] public CharacterDetailsSO characterDetails;
    [HideInInspector] public string characterName;
    [HideInInspector] public int speed;
    [HideInInspector] public int health;
    [HideInInspector] public float armorClass;
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
    /// Initialize the character
    /// </summary>
    public void Initialize(CharacterDetailsSO characterDetails)
    {
        this.characterDetails = characterDetails;
        characterName = characterDetails.characterName;
        health = characterDetails.characterHealthAmount;
        speed = characterDetails.characterSpeed;
        armorClass = characterDetails.characterArmorClass;


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