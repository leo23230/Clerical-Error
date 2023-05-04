using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationCues : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIsAttackingAXEToFalse()
    {
        animator.SetBool("isAttackingAXE", false);
        //SetIsReadyingToTrue();
    }
    public void SetIsAttackingSPRToFalse()
    {
        animator.SetBool("isAttackingSPR", false);
        //SetIsReadyingToTrue();
    }
    public void SetIsAttackingXBWToFalse()
    {
        animator.SetBool("isAttackingXBW", false);
        //SetIsReadyingToTrue();
    }
    public void SetIsReadyingToTrue()
    {
        animator.SetBool("isReadying", true);
    }

    public void SetIsReadyingToFalse()
    {
        animator.SetBool("isReadying", false);
    }
}
