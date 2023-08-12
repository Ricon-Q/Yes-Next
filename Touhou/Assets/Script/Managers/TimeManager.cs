using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;

    [System.Serializable]
    public class Time
    {
        public int minute = 0;
        public int hour = 0;
        public int day = 1;
        public int month = 1;
    }

    public Time time;

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
        time.minute += minute;

        if(time.minute >= 60)
        {
            time.minute = time.minute - 60;
            increaseHour(1);
        }
    }

    public void increaseHour(int hour)
    {
        time.hour += hour;

        if(time.hour >= 24)
        {
            time.hour = time.hour - 24;
            increaseDay(1);
        }
    }

    public void increaseDay(int day)
    {
        time.day += day;
    }
}
