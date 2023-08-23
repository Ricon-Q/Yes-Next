using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 대화 데이터
public class TalkManager : MonoBehaviour
{
    private static TalkManager instance;
    public static TalkManager Instance
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
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }    
        else { Destroy(this.gameObject); }

        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    Dictionary<int, string[]> talkData;

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "이것은 NPC_Shop입니다", "상점 기능을 담당합니다." });
        talkData.Add(2000, new string[] { "이것은 NPC_Talk입니다", "일반 대화 기능을 담당합니다." });
        talkData.Add(3000, new string[] { "이것은 NPC_Quest입니다", "퀘스트 기능을 담당합니다." });
        talkData.Add(100, new string[] { "이것은 작물입니다" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
