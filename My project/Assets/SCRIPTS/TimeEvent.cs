using UnityEngine.Events;

[System.Serializable]
public class OnHourEvent
{
    public int Hour;
    public UnityEvent CustomEvent = new UnityEvent();
}

[System.Serializable]
public class OnDayEvent
{
    public int Day;
    public UnityEvent CustomEvent = new UnityEvent();
}

[System.Serializable]
public class OnWeekEvent
{
    public int Week;
    public UnityEvent CustomEvent = new UnityEvent();
}

// [System.Serializable]
// public class OnDayAndHourEvent
// {
//     public int Day;
//     public int Hour;
//     public UnityEvent CustomEvent = new UnityEvent();
// }