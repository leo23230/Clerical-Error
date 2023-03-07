using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBarManager : MonoBehaviour
{

    public Image lightCooldownBarImage;
    public Image heavyCooldownBarImage;
    public Image specialCooldownBarImage;

    [HideInInspector] public CharacterStateManager character;
    [HideInInspector] public List<Ability> abilities = new List<Ability>();

    private void Start()
    {
        character = GetComponent<CharacterStateManager>();
        abilities = character.abilities;
    }

    //temporary garbage code//
    private void Update()
    {
        UpdateAllCoolDownBars();
    }
    public void UpdateAllCoolDownBars()
    {
        foreach(Ability ability in abilities)
        {
            if (ability.name == "Light") 
            {
                if (ability.AbilityIsReady())
                {
                        ResetCoolDownBar(lightCooldownBarImage);
                }
                else
                {
                    UpdateCoolDownBar(lightCooldownBarImage, ability.coolDown, ability.coolDownTime);
                }
            } 
            else if (ability.name == "Heavy")
            {
                if (ability.AbilityIsReady())
                {
                    ResetCoolDownBar(heavyCooldownBarImage);
                }
                else
                {
                    UpdateCoolDownBar(heavyCooldownBarImage, ability.coolDown, ability.coolDownTime);
                }
            }
            else if (ability.name == "Special")
            {
                    if (ability.AbilityIsReady())
                    {
                        ResetCoolDownBar(specialCooldownBarImage);
                    }
                    else
                    {
                        UpdateCoolDownBar(specialCooldownBarImage, ability.coolDown, ability.coolDownTime);
                    }
            }
        }
    }
    //

    public void UpdateCoolDownBar(Image bar, float currentTime, float coolDownTime)
    {
        bar.fillAmount = (coolDownTime-currentTime) / coolDownTime;
    }

    public void ResetCoolDownBar(Image bar)
    {
        bar.fillAmount = 1f;
    }
}
