using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour, IDataPersistence
{
    private static TimeManager instance;
    public TimeData timeData;

    public Time time;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static TimeManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
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
    }

    public void LoadData(GameData data)
    {
        this.timeData = data.timeData;
    }

    public void SaveData(ref GameData data)
    {
        data.timeData = this.timeData;
    }
}
