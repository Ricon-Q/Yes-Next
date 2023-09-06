using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour, IDataPersistence
{
    private static NPCManager instance;
    public static NPCManager Instance
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
            // DontDestroyOnLoad(this.gameObject);
        }    
        else { Destroy(this.gameObject); }
    }
    
    public List<NpcData> npcDatas;

    public void LoadData(GameData data)
    {
        this.npcDatas = data.npcDatas;
    }

    public void SaveData(ref GameData data)
    {
        data.npcDatas = this.npcDatas;
    }

    public void AddOrUpdate(NpcData newData)
    {
        for (int i = 0; i < npcDatas.Count; i++)
        {
            if (npcDatas[i].name == newData.name)
            {
                // If an NPC with the same name is found, update the data
                npcDatas[i] = newData;
                return;  // terminate the AddOrUpdate method after updating the data
            }
        }

        // If execution reaches this point, no matching name was found in the list
        // Hence, the new NpcData is added to the list
        npcDatas.Add(newData);
    }

    public NpcData LoadNpcData(string name)
    {
        for (int i = 0; i < npcDatas.Count; i++)
        {
            if (npcDatas[i].name == name)
            {
                return npcDatas[i];
            }
        }
        return null;
    }
}
