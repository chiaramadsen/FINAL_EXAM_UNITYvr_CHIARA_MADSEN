using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayEventManager : MonoBehaviour
{
    [Tooltip("These events will trigger on a specific day each game week")]
    public List<OnDayEvent> OnSpecificDayEvents = new List<OnDayEvent>();
    
    // Start is called before the first frame update
    void Start()
    {
        var gameTimeManager = GameTimeManager.Instance;
        
        gameTimeManager.OnDayPassed += HandleOnDayEvents;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void HandleOnDayEvents(int day)
    {
        foreach (var eventItem in OnSpecificDayEvents)
        {
            if (eventItem.Day == day)
            {
                eventItem.CustomEvent.Invoke();
            }
        }
    }
    
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
        
        gameTimeManager.OnDayPassed -= HandleOnDayEvents;

    }
}
