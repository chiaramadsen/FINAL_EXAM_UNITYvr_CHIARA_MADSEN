using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class GameTimeView : MonoBehaviour
{
    public GameTimeController controller;
    
    public TMP_Text HourText;
    public TMP_Text DayText;
    public TMP_Text WeekText;
    


    private void Start()
    {
        //controller.GameTime.OnHourPassed += UpdateTimeText;
        //controller.GameTime.OnDayPassed += UpdateTimeText;
        //controller.GameTime.OnWeekPassed += UpdateTimeText;
    }

    private void UpdateTimeText()
    {
        HourText.text = controller.GameTime.CurrentHour.ToString();
        DayText.text = controller.GameTime.CurrentDay.ToString();
        WeekText.text = controller.GameTime.CurrentWeek.ToString();
    }

    private void Update()
    {
        UpdateTimeText();
    }
}
    