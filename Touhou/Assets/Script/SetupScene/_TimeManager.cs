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
            DontDestroyOnLoad(this.gameObject);
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
        this.hour = 0;
        this.minute = 0;
    }
}
