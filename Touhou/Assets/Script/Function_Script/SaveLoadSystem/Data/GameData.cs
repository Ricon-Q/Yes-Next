using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public _PlayerData playerData;
    public TimeData timeData;
    public List<CropData> cropDatas;
    public List<NpcData> npcDatas;

    // 해당 생성자들의 값들은 기본 값이다
    // 데이터에 로드할 것이 없을때 아래 값들을 가져온다
    public GameData()
    {
        this.playerData = new _PlayerData();
        this.timeData = new TimeData();
        // this.cropDatas = new SerializableDictionary<string, List<CropData>>();
        this.cropDatas = new List<CropData>();
        this.npcDatas = new List<NpcData>();
    }
}

[System.Serializable]
public class _PlayerData
{
    public float maxHealth;        // 최대 체력
    public float currentHealth;    // 현재 체력
    public float maxFatigue;       // 최대 피로도
    public float currentFatigue;   // 현재 피로도
    public float maxHunger;        // 최대 만복도
    public float currentHunger;    // 현재 만복도
    public long money;
    public long hospitalLevel;

    public _PlayerData()
    {
        this.maxHealth = 100;
        this.currentHealth = 100;    
        this.maxFatigue = 100;       
        this.currentFatigue = 100;   
        this.maxHunger = 100;        
        this.currentHunger = 100;    
        this.money = 50000;
        this.hospitalLevel = 1;
    }
}

[System.Serializable]
public class TimeData
{
    public int minute;
    public int hour;
    public int day;
    public int month;

    public TimeData()
    {
        this.minute = 1;
        this.hour = 2;
        this.day = 3;
        this.month = 4;
    }
}

[System.Serializable]
public class NpcData
{
    public string name;
    public Vector3 position;
    public int affection;
    public string sceneName;
    
    public NpcData(string name, Vector3 position, int affection, string sceneName)
    {
        this.name = name;
        this.position = position;
        this.affection = affection;
        this.sceneName = sceneName;
    }
}

[System.Serializable]
public class CropData
{
    public int plantedDay;
    public Vector3 position;
    public string name;
    public string sceneName;
    public CropData(int plantedDay, Vector3 position, string name, string sceneName)
    {
        this.plantedDay = plantedDay;
        this.position = position;
        this.name = name;
        this.sceneName = sceneName;
    }
}