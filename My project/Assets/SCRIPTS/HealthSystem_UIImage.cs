using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem_UIImage : MonoBehaviour
{
    //Health System Data    
    public int CurrentHealth;
    
    public int MaxHealth = 100;
    
    public int MinHealth = 0;
    
    public Image HealthBar;
    
    public int DefenseRating = 1;
    
    // Health System Methods
    void Start()
    {
        
    }

    // private void Update()
    // {
    //     UpdateUI();
    // }

    public void SetDefenseRating(int rating)
    {
        DefenseRating = rating;
    }

    public void Damage(int damage)
    {
        CurrentHealth -= (damage - DefenseRating);

        //use mathf.clamp to keep the health within the min and max health
        CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealth, MaxHealth);

        UpdateUI();
    }

    public void Heal(int heal)
    {
        CurrentHealth += heal;

        //use mathf.clamp to keep the health within the min and max health
        CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealth, MaxHealth);

        UpdateUI();
    }

    public void RestoreFullHealth()
    {
        CurrentHealth = MaxHealth;

        UpdateUI();
    }


    //method to update a slide for the health bar
    public void UpdateUI()
    {
        var normalizedHealth = (float)CurrentHealth / MaxHealth;

        HealthBar.fillAmount = normalizedHealth;
    }


}
