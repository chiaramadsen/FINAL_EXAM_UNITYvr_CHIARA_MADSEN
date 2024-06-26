using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeEventManager : MonoBehaviour
{
    [Header("Event Lists")]
    [Tooltip("These events will trigger every hour")]
    public List<UnityEvent> HourlyEvents = new List<UnityEvent>();
    
    [Tooltip("These events will trigger every day")]
    public List<UnityEvent> DailyEvents = new List<UnityEvent>();
    
    [Tooltip("These events will trigger every week")]
    public List<UnityEvent> WeeklyEvents = new List<UnityEvent>();

    [Tooltip("These events will trigger on a specific hour each game day")]
    public List<OnHourEvent> OnSpecificHourEvents = new List<OnHourEvent>();
    
    [Tooltip("These events will trigger on a specific day each game week")]
    public List<OnDayEvent> OnSpecificDayEvents = new List<OnDayEvent>();

    [Tooltip("These events will trigger on a specific week each game month")]
    public List<OnWeekEvent> OnSpecificWeekEvents = new List<OnWeekEvent>();

    // [Tooltip("These events will trigger on specific days and hours")]
    // public List<OnDayAndHourEvent> OnDayAndHourEvents = new List<OnDayAndHourEvent>();

    private void Start()
    {
        var gameTimeManager = GameTimeManager.Instance;

        gameTimeManager.OnHourPassed += HandleOnHourEvents;
        //gameTimeManager.OnDayPassed += HandleOnDayAndHourEvents;
        gameTimeManager.OnDayPassed += HandleOnDayEvents;
        gameTimeManager.OnWeekPassed += HandleOnWeekEvents;
    }

    private void HandleOnHourEvents(int hour)
    {
        TriggerEvents(HourlyEvents);

        foreach (var eventItem in OnSpecificHourEvents)
        {
            if (eventItem.Hour == hour)
            {
                eventItem.CustomEvent.Invoke();
            }
        }

        // Trigger day and hour specific events
        var currentDay = GameTimeManager.Instance.GetCurrentDay();
        //HandleOnDayAndHourEvents(currentDay, hour);
    }

    private void HandleOnDayEvents(int day)
    {
        TriggerEvents(DailyEvents);

        foreach (var eventItem in OnSpecificDayEvents)
        {
            if (eventItem.Day == day)
            {
                eventItem.CustomEvent.Invoke();
            }
        }
    }

    private void HandleOnWeekEvents(int week)
    {
        TriggerEvents(WeeklyEvents);

        foreach (var eventItem in OnSpecificWeekEvents)
        {
            if (eventItem.Week == week)
            {
                eventItem.CustomEvent.Invoke();
            }
        }
    }

    // private void HandleOnDayAndHourEvents(int day, int hour)
    // {
    //     foreach (var eventItem in OnDayAndHourEvents)
    //     {
    //         if (eventItem.Day == day && eventItem.Hour == hour)
    //         {
    //             eventItem.CustomEvent.Invoke();
    //         }
    //     }
    // }

    private void TriggerEvents(List<UnityEvent> events)
    {
        foreach (var eventItem in events)
        {
            eventItem.Invoke();
        }
    }

    private void OnDestroy()
    {
        var gameTimeManager = GameTimeManager.Instance;

        gameTimeManager.OnHourPassed -= HandleOnHourEvents;
        gameTimeManager.OnDayPassed -= HandleOnDayEvents;
        gameTimeManager.OnWeekPassed -= HandleOnWeekEvents;
    }
}
