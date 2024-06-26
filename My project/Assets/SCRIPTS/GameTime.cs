using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class GameTime
{
    [Range(0,1)]
    public float TimeOfDay; // 0.0f to 1.0f, where 0.0f is midnight, 0.5f is noon, etc.
    //public float DayDuration = 120f; // Length of a day in seconds
    
    public int CurrentHour = 0;
    public int CurrentDay = 0;
    public int CurrentWeek = 0;

    // Events to broadcast when the hour, day, or week changes
    public event Action<int> OnHourPassed;
    public event Action<int> OnDayPassed;
    public event Action<int> OnWeekPassed;
    
    // Update the time of day based on the delta time and day duration
    public void UpdateTime(float deltaTime, float dayDuration)
    {
        // Store the previous time and hour
        float previousTime = TimeOfDay;
        int previousHour = GetCurrentHour();

        // Calculate the new time of day
        TimeOfDay += deltaTime / dayDuration;

        // Check if the day has changed
        if (TimeOfDay >= 1.0f)
        {
            TimeOfDay -= 1.0f;
            CurrentDay++;
            OnDayPassed?.Invoke(CurrentDay);
            
            // Check if the week has changed
            if (CurrentDay % 7 == 0)
            {
                CurrentWeek++;
                OnWeekPassed?.Invoke(CurrentWeek);
            }
        }

        // Calculate the new hour
        CurrentHour = GetCurrentHour();
        
        // Check if the hour has changed
        if (previousHour != CurrentHour)
        {
            OnHourPassed?.Invoke(CurrentHour);
        }
    }

    // Helper method to get the current hour based on the time of day
    public int GetCurrentHour() => Mathf.FloorToInt(TimeOfDay * 24);
}