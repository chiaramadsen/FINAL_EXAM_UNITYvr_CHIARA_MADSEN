using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
public class AttackAbility : MonoBehaviour
{
    public string InputActionName;
    public string AnimatorTriggerName;
    public float AnimationDuration = 3.5f;
    
    public GameObject prefab;
    public GameObject spawnPosition;
    public float destroyDelay = 2.0f;

    public GameObject Player;
    
    public PlayerInput PlayerInput;
    public Animator Animator;

    public ThirdPersonController ThirdPCharController;
    public CharacterController CharacterController;
    
    private InputAction InputAction;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerInput = GetComponent<PlayerInput>();
        //Animator = GetComponent<Animator>();

        //ThirdPCharController = GetComponent<ThirdPersonController>();
        //CharacterController = GetComponent<CharacterController>();

        if (PlayerInput != null)
        {
            InputAction = PlayerInput.actions.FindAction(InputActionName);
            if (InputAction != null)
            {
                InputAction.performed += ActionEvent;
            }
        }
    }

    private void ActionEvent(InputAction.CallbackContext obj)
    {
        if (Animator != null)
        {
            Animator.SetTrigger(AnimatorTriggerName);
            StartCoroutine(AnimationCoroutine());

            // Instantiate the prefab at the spawn position object's position with the player's orientation
            GameObject spawnedObject = Instantiate(prefab, spawnPosition.transform.position, spawnPosition.transform.rotation);

            // Destroy the spawned object after a predefined amount of time
            Destroy(spawnedObject, destroyDelay);
        }
    }

    private IEnumerator AnimationCoroutine()
    {
        // Turn off the controllers
        if (ThirdPCharController != null)
        {
            ThirdPCharController.enabled = false;
        }
        if (CharacterController != null)
        {
            CharacterController.enabled = false;
        }

        // Wait for the animation to end
        yield return new WaitForSeconds(AnimationDuration);

        // Turn the controllers back on
        if (ThirdPCharController != null)
        {
            ThirdPCharController.enabled = true;
        }
        if (CharacterController != null)
        {
            CharacterController.enabled = true;
        }
    }
}
