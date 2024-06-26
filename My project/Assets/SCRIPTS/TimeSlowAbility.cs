using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeSlowAbility : MonoBehaviour
{
    public float TimeSlow = 0.1f;
    public string TimeSlowActionName = "TimeSlow";
    
    private PlayerInput PlayerInput;
    private InputAction timeSlowAction;

    // |Input Defintion
    void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();

        timeSlowAction = PlayerInput.actions.FindAction(TimeSlowActionName);

        timeSlowAction.performed += OnTimeSlowActive;

        timeSlowAction.canceled += OnTimeSlowInactive;
    }

    // |Trigger Logic
    void OnTimeSlowActive(InputAction.CallbackContext context)
    {
        Time.timeScale = TimeSlow;
    }

    void OnTimeSlowInactive(InputAction.CallbackContext context)
    {
        Time.timeScale = 1f;
    }

}
