using UnityEngine;
using UnityEngine.Events;

public class KeyPressTrigger : MonoBehaviour
{
    [SerializeField]
    private KeyCode triggerKey = KeyCode.E; // Default key is Space

    [SerializeField]
    private UnityEvent OnKeyPressedEvent;

    [SerializeField]
    private UnityEvent OnKeyReleasedEvent;

    private void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            if (OnKeyPressedEvent != null)
            {
                OnKeyPressedEvent.Invoke();
            }
            else if (OnKeyReleasedEvent != null)
            {
                OnKeyReleasedEvent.Invoke();
            }
            else
            {
                Debug.LogWarning("No event assigned to KeyPressTrigger");
            }
        }
    }
}
