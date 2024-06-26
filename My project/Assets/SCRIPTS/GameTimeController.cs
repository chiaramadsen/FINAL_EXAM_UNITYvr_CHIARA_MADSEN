using System;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [SerializeField] private float dayDurationInSeconds = 120f; // Length of a day in seconds
    
    [Header("Time System (Read Only)")]
    public GameTime GameTime;

    void Start()
    {
        GameTime = new GameTime();
    }
    
    private void Update()
    {
        GameTime.UpdateTime(Time.deltaTime, dayDurationInSeconds);
    }
}