using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    private int startingHealth;
    private int currentHealth;

    public Image healthBarImage;

    /// <summary>
    /// Set starting health 
    /// </summary>
    public void SetStartingHealth(int startingHealth)
    {
        this.startingHealth = startingHealth;
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Get the starting health
    /// </summary>
    public int GetStartingHealth()
    {
        return startingHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void SubtractHealth(int amt)
    {
        if(currentHealth - amt < 0)
        {
            currentHealth = -1;
        }
        else
        {
            currentHealth -= amt;
        }

        UpdateHealthBar();
    }

    public void AddHealth(int amt)
    {
        if(currentHealth <= startingHealth - amt)
        {
            currentHealth += amt;
        }
        else
        {
            currentHealth = startingHealth;
        }
        
        UpdateHealthBar();
    }

    public void UpdateHealthBar() 
    {
        healthBarImage.fillAmount = (float)currentHealth / (float)startingHealth;
    }

}