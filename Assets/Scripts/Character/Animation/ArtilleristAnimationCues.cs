using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleristAnimationCues : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIsAttackingToFalse()
    {
        animator.SetBool("isAttacking", false);
    }
}
