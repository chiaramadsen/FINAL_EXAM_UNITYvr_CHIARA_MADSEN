using UnityEngine;
using UnityEngine.Events;
using System;

[System.Serializable]
public class State : MonoBehaviour
{
    // [Header("Info")]
    // public string stateName;
    
    // Delegate actions can be overridden by derived classes if needed.
    public Action OnStateEnter;
    public Action OnStateUpdate;
    public Action OnStateExit;
    
    // Unity events for more editor-driven flexibility.
    public UnityEvent onEnter;
    public UnityEvent onUpdate;
    public UnityEvent onExit;

    // Make the Enter method virtual to allow derived classes to override it.
    public virtual void Enter()
    {
        onEnter.Invoke(); // Trigger the UnityEvent.
        
        //Debug.Log($"Entering {gameObject.name} State");
        
        OnStateEnter?.Invoke(); // Trigger the delegate action if assigned.
    }

    // Make the UpdateState method virtual to allow derived classes to override it.
    public virtual void UpdateState()
    {
        onUpdate.Invoke(); // Trigger the UnityEvent.
        OnStateUpdate?.Invoke(); // Trigger the delegate action if assigned.
    }

    // Make the Exit method virtual to allow derived classes to override it.
    public virtual void Exit()
    {
        onExit.Invoke(); // Trigger the UnityEvent.
        //Debug.Log($"Exiting {gameObject.name} State");
        OnStateExit?.Invoke(); // Trigger the delegate action if assigned.
    }
}
