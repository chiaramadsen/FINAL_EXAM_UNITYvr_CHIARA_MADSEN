using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damageAmount = 10f; // You can use this to adjust damage if needed

    private void OnTriggerEnter(Collider other)
    {
        Damagable damagable = other.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damageAmount);
        }
    }
}