using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{
    public int EnergyValue = 80;
    
    public int MaxEnergyValue = 80;
    
    public Slider EnergySlider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //equate the fill value of the restslider to the rest value
        EnergySlider.value = EnergyValue;
    }
    
    void RestoreRest()
    {
        EnergyValue = 100;
    }

    public void Rest(int amount)
    {
        EnergyValue += amount;
        if (EnergyValue >= MaxEnergyValue)
        {
            EnergyValue = MaxEnergyValue;
        }
    }
    
    public void Tire(int amount)
    {
        EnergyValue -= amount;
        if (EnergyValue <= 0)
        {
            EnergyValue = 0;
        }
    }
    
    public void Sleep()
    {
        // Calculate the missing rest points
        int missingRest = MaxEnergyValue - EnergyValue;

        // Determine the number of hours to sleep (1 hour for each 10 points of rest)
        int sleepHours = missingRest / 10;

        // Add the sleep hours to the game time
        GameTimeManager.Instance.AddHours(sleepHours);

        // Replenish the rest value
        EnergyValue = MaxEnergyValue;
    }
    
}
