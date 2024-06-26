using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // [Header("Info")]
    // public string stateMachineName;
    
    [Header("Config")]
    public State initialState;
    
    // Protected so it can be accessed by derived classes
    protected State currentState;

    protected virtual void Start()
    {
        ChangeState(initialState);
    }

    protected virtual void Update()
    {
        currentState?.UpdateState();
    }

    // Virtual allows it to be overridden to add additional behavior.
    public virtual void ChangeState(State newState)
    {
        // Invoke virtual methods for state exit and entry, which can be overridden.
        OnStateExit();
        currentState = newState;
        OnStateEnter();
        
        Debug.Log($"Changing to {currentState.gameObject.name} State");
    }

    // Protected virtual method for state exit behavior, allowing overrides.
    protected virtual void OnStateExit()
    {
        currentState?.Exit();
    }

    // Protected virtual method for state enter behavior, allowing overrides.
    protected virtual void OnStateEnter()
    {
        currentState?.Enter();
    }
}