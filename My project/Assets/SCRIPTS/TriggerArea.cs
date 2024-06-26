using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{

    public string TagToCheck = "Player";

    public UnityEvent OnEnterEvent;
    public UnityEvent OnExitEvent;


    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(TagToCheck))
        {
            OnEnterEvent.Invoke();
        }
    }

    // This method is called when another collider exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag(TagToCheck))
        {
            OnExitEvent.Invoke();
        }
    }
}
