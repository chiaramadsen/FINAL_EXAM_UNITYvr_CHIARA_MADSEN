using System;
using UnityEngine;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    #region Singleton Implementation
    ///////////////////////////////// Singleton Implementation /////////////////////////////////
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    #endregion
    
    ////////////////////////////////// Public Variables /////////////////////////////////
    [Header("Game State Machine")]
    public StateMachine gameStateMachine;
    public State GameLoseState;
    public State GameWinState;

    public int NumberOfDaysToSurvive = 6;
    public HealthSystem_Building Monument;

    ////////////////////////////////// Unity Methods /////////////////////////////////
    private void Start()
    {

    }

    private void Update()
    {
        GameLoseStateCheck();
        GameWinStateCheck();
    }
    
    ////////////////////////////////// State Methods /////////////////////////////////
    
    public void GameWinStateCheck()
    {
        if (GameTimeManager.Instance.currentDay >= NumberOfDaysToSurvive)
        {
            gameStateMachine.ChangeState(GameWinState);
        }
    }
    
    public void GameLoseStateCheck() //Check if monument is dead
    {
        if (Monument.isDead)
        {
            gameStateMachine.ChangeState(GameLoseState);
        }
    }


    
    ////////////////////////////////// Control Methods /////////////////////////////////

    ///////////////////////////////// Visualization Methods /////////////////////////////////
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}