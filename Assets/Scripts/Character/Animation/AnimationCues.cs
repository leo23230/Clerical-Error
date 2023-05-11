using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCues : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIsAttackingToFalse()
    {
        animator.SetBool("isAttacking", false);
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

    public void ArtilleristShake()
    {
        if(GameObject.Find("CameraTarget").transform.position.y < 9f)
        {
            ScreenShake.Instance.ShakeCamera(10f, .2f, true);
        }
    }
    public void GreatShieldShake()
    {
        if (GameObject.Find("CameraTarget").transform.position.y < 9f)
        {
            ScreenShake.Instance.ShakeCamera(3f, .1f, true);
        }
    }
}
