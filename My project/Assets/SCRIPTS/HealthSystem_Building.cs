using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem_Building : MonoBehaviour
{
    ///////////////////////// Public Variables ////////////////////////////

    [Range(0,100)]
    public int CurrentHealth;
    public int MaxHealth = 100;
    public int MinHealth = 0;

    public float SunkYPosition = -1f; // Define the y value for the sunk position

    public GameObject BuildingSpot;

    public bool isDead = false;

    ///////////////////////// Private Variables //////////////////////////

    private Renderer[] allRenderers;
    private Vector3 originalPosition;

    ///////////////////////// Unity Methods /////////////////////////////

    void Start()
    {
        allRenderers = GetComponentsInChildren<Renderer>();
        originalPosition = transform.position; // Save the original position
        UpdateHealthVisuals();
    }

    private void Update()
    {
        UpdateHealthVisuals();
        
    }

    ///////////////////////// Control Methods //////////////////////////

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealth, MaxHealth);
        UpdateHealthVisuals();
        Die();
    }

    public void Heal(int heal)
    {
        CurrentHealth += heal;
        CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealth, MaxHealth);
        UpdateHealthVisuals();
    }

    public void Die()
    {
        if (CurrentHealth <= MinHealth)
        {
            isDead = true;
            //Destroy(gameObject);
            //gameObject.SetActive(false);

            if (BuildingSpot != null) BuildingSpot.SetActive(true);
        }
    }

    ///////////////////////// Visualization Methods /////////////////////

    private void UpdateHealthVisuals()
    {
        if (CurrentHealth > 50)
        {
            // Color transition from white to black as health goes from 100 to 50
            
            // Calculate the new color using the InverseLerp + Lerp value remapping technique
            float colorValue = Mathf.InverseLerp(MaxHealth, 50, CurrentHealth);
            Color newColor = Color.Lerp(Color.white, Color.black, colorValue);
            
            // Update the color
            UpdateMaterialColor(newColor);
        }
        else
        {
            // Sinking effect as health goes from 50 to 0
            
            //Calculate the new position using the InverseLerp + Lerp value remapping technique
            float sinkValue = Mathf.InverseLerp(50, MinHealth, CurrentHealth);
            Vector3 newPosition = new Vector3(originalPosition.x, Mathf.Lerp(originalPosition.y, SunkYPosition, sinkValue), originalPosition.z);
            
            // Update the position
            transform.position = newPosition;

            // Keep materials black when sinking
            UpdateMaterialColor(Color.black);
        }
    }

    private void UpdateMaterialColor(Color color)
    {
        foreach (Renderer renderer in allRenderers)
        {
            foreach (Material mat in renderer.materials)
            {
                mat.color = color;
            }
        }
    }
}
