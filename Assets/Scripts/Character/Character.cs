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
    [HideInInspector] public CharacterDetailsSO characterDetails;
    [HideInInspector] public Health health;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        // Load components
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Initialize the character
    /// </summary>
    public void Initialize(CharacterDetailsSO characterDetails)
    {
        this.characterDetails = characterDetails;

        // Set character starting health
        SetCharacterHealth();
    }

    /// <summary>
    /// Set character health from characterDetails SO
    /// </summary>
    private void SetCharacterHealth()
    {
        health.SetStartingHealth(characterDetails.characterHealthAmount);
    }

}