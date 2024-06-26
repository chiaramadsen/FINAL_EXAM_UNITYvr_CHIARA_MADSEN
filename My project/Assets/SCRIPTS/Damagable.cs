using UnityEngine;

public class Damagable : MonoBehaviour
{
    public GameObject objectToDestory;
    
    public GameObject objectToSpawnOnDeath; // Object to spawn on death

    public void TakeDamage(float damageAmount)
    {
        // Spawn the object if it is set
        if (objectToSpawnOnDeath != null)
        {
            Instantiate(objectToSpawnOnDeath, transform.position, transform.rotation);
        }
        
        // use the damageAmount to reduce health from a health_system script?
        
        // Handle any logic before destroying the object, like playing a sound or animation
        Destroy(objectToDestory);
    }
}