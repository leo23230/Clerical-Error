using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertMonobehaviour : MonoBehaviour
{

    private GameObject animations;
    private Animator animator;
    private string lowHealthAnimatorBool = "isLowHealth";
    private string criticalAnimatorBool = "isCritical";
    private string deadAnimatorBool = "isDead";
    private string fineAnimatorBool = "isFine";

    public List<Sprite> alertSprites = new List<Sprite>();

    //Colors
    private Color transparent = new Color(0f, 0f, 0f, 0f);

    private void Awake()
    {

        animations = transform.Find("Animations").gameObject;
        animator = animations.GetComponent<Animator>();
        animator.SetBool(fineAnimatorBool, true);
    }

    public void UpdatePosition(float _x)
    {
        Vector3 newPos = new Vector3(_x, transform.position.y, transform.position.z);
        transform.position = newPos;
    }

    //temporary function for swapping sprites
    public void UpdateSprite(float _health)
    {
        Debug.Log(_health);
        if (_health > 20f && _health <= 40f)
        {
            Debug.Log("Health Low");

            SwitchAnimations(lowHealthAnimatorBool);
        }
        else if(_health > 0f && _health <= 20f)
        {
            Debug.Log("Critical");

            SwitchAnimations(criticalAnimatorBool);
        }
        else if(_health <= 0f)
        {

            SwitchAnimations(deadAnimatorBool);
        }
        else
        {
            SwitchAnimations(fineAnimatorBool);
        }
    }

    public void SwitchAnimations(string _animation)
    {
        if(_animation == lowHealthAnimatorBool)
        {
            animator.SetBool(lowHealthAnimatorBool, true);
            animator.SetBool(criticalAnimatorBool, false);
            animator.SetBool(deadAnimatorBool, false);
            animator.SetBool(fineAnimatorBool, false);
        }
        else if(_animation == criticalAnimatorBool)
        {
            animator.SetBool(lowHealthAnimatorBool, false);
            animator.SetBool(criticalAnimatorBool, true);
            animator.SetBool(deadAnimatorBool, false);
            animator.SetBool(fineAnimatorBool, false);
        }
        else if (_animation == deadAnimatorBool)
        {
            animator.SetBool(lowHealthAnimatorBool, false);
            animator.SetBool(criticalAnimatorBool, false);
            animator.SetBool(deadAnimatorBool, true);
            animator.SetBool(fineAnimatorBool, false);
        }
        else if (_animation == fineAnimatorBool)
        {
            animator.SetBool(lowHealthAnimatorBool, false);
            animator.SetBool(criticalAnimatorBool, false);
            animator.SetBool(deadAnimatorBool, false);
            animator.SetBool(fineAnimatorBool, true);
        }
    }
}
