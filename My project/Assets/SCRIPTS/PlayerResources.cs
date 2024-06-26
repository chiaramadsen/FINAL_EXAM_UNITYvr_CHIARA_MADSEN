using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    ///////////////////////// Public Variables ////////////////////////////
    public int Wood = 0;
    
    ///////////////////////// Private Variables //////////////////////////
    
    
    ///////////////////////// Unity Methods /////////////////////////////
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    ///////////////////////// Control Methods //////////////////////////
   
    public void AddWood(int amount)
    {
        Wood += amount;
    }
    
    public void RemoveWood(int amount)
    {
        Wood -= amount;
    }
    
}
