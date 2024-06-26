using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimeManager : MonoBehaviour
{
    ///////////////////////// Public Variables ////////////////////////////
    public static GameTimeManager Instance { get; private set; }

    [Header("Time Configuration")]
    [SerializeField] private float dayDurationInSeconds = 120f; // Length of a day in seconds
    
    // Events to broadcast when the hour, day, or week changes
    public event Action<int> OnHourPassed;
    public event Action<int> OnDayPassed;
    public event Action<int> OnWeekPassed;

    ///////////////////////// Private Variables //////////////////////////
    [Header("Time Tracking, Read-Only")]
    [SerializeField, Range(0, 1)]
    public float timeOfDay; // 0.0f to 1.0f, where 0.0f is midnight, 0.5f is noon, etc.
    public int currentHour;
    public int currentDay;
    public int currentWeek;

    ///////////////////////// Unity Methods /////////////////////////////
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject); // Destroy this gameObject if a duplicate instance exists
        } 
        else 
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Make this singleton persist across scenes
        }
    }
    
    private void Start()
    {
        UpdateTimeText(); // Update the UI with the initial time
    }

    private void Update()
    {
        UpdateTime(Time.deltaTime);
    }

    ///////////////////////// Control Methods //////////////////////////
    private void UpdateTime(float deltaTime)
    {
        float previousTime = timeOfDay;
        int previousHour = GetCurrentHour();

        timeOfDay += deltaTime / dayDurationInSeconds;

        if (timeOfDay >= 1.0f)
        {
            timeOfDay -= 1.0f;
            currentDay++;
            OnDayPassed?.Invoke(currentDay); // Trigger the OnDayPassed event
            if (currentDay % 7 == 0)
            {
                currentWeek++;
                OnWeekPassed?.Invoke(currentWeek); // Trigger the OnWeekPassed event
            }
            UpdateTimeText(); // Update the UI whenever the day or week changes.
        }

        currentHour = GetCurrentHour();
        if (previousHour != currentHour)
        {
            OnHourPassed?.Invoke(currentHour); // Trigger the OnHourPassed event
            UpdateTimeText(); // Update the UI whenever the hour changes.
        }
    }

    public int GetCurrentHour() => Mathf.FloorToInt(timeOfDay * 24);
    public int GetCurrentDay() => currentDay;
    public int GetCurrentWeek() => currentWeek;

    ///////////////////////// Visualization Methods ////////////////////////
    [Header("UI Elements")]
    [SerializeField] private TMP_Text timeText; // Reference to the UI Text component for displaying time.

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = $"Hour: {currentHour}, Day: {currentDay}, Week: {currentWeek}";
        }
    }
}
