using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TimeManager : MonoBehaviour
{
    private static _TimeManager instance;

    public static _TimeManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // ======================================================== //

    public _TimeData timeData;

    private void Start() 
    {
        timeData = new _TimeData();    
    }

    public void SetTargetTimeHour(int targetHour)
    {
         if (timeData.hour >= targetHour)
        {
            // If yes, set the target hour for the next day
            SetTimeData(timeData.year, timeData.month, timeData.day + 1, targetHour, 0);
        }
        else
        {
            // If not, set the target hour for the current day
            SetTimeData(timeData.year, timeData.month, timeData.day, targetHour, 0);
        }
    }


    public void increaseMinute(int minute)
    {
        timeData.minute += minute;

        if(timeData.minute >= 60)
        {
            timeData.minute = timeData.minute - 60;
            increaseHour(1);
        }
    }

    public void increaseHour(int hour)
    {
        timeData.hour += hour;

        if(timeData.hour >= 24)
        {
            timeData.hour = timeData.hour - 24;
            increaseDay(1);
        }
    }

    public void increaseDay(int day)
    {
        timeData.day += day;
        if(timeData.day > 28)
        {
            timeData.day = timeData.day - 28;
            increaseMonth(1);
        }
    }

    public void increaseMonth(int month)
    {
        timeData.month += month;
        if(timeData.month > 4)
        {
            timeData.month = timeData.month - 4;
            increaseYear(1);
        }
    }

    public void increaseYear(int year)
    {
        timeData.year += year;
    }

    private void SetTimeData(int year, int month, int day, int hour, int minute)
    {
        timeData.year = year;
        timeData.month = month;
        timeData.day = day;
        timeData.hour = hour;
        timeData.minute = minute;
    }

    public int DaysSince(_TimeData other)
    {
        // 간단한 예제로, 실제 애플리케이션에서는 더 정확한 날짜 계산 로직이 필요할 수 있습니다.
        // 여기서는 각 월을 30일로 가정하고 계산합니다.
        int daysThis = timeData.year * 28*4 + timeData.month * 28 + timeData.day;
        int daysOther = other.year * 28*4 + other.month * 28 + other.day;
        return daysThis - daysOther;
    }
}

public class _TimeData
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;

    public _TimeData()
    {
        this.year = 1;
        this.month = 1;
        this.day = 1;
        this.hour = 6;
        this.minute = 0;
    }
    public _TimeData(_TimeData timeData)
    {   
        this.year = timeData.year;
        this.month = timeData.month;
        this.day = timeData.day;
        this.hour = timeData.hour;
        this.minute = timeData.minute;
    }
}
