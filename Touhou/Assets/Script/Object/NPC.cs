using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    // public string name;
    public bool isShopable;
    public Sprite portrait;
    // public InventoryObject inventoryObject;
    public NpcData npcData;
    
    private void Awake()
    {
        // npcData = new NpcData(
        //                         gameObject.name, 
        //                         gameObject.transform.position, 
        //                         0, 
        //                         SceneManager.GetActiveScene().name
        //                     );
        npcData = NPCManager.Instance.LoadNpcData(gameObject.name);
        if(npcData == null)
        {
            npcData = new NpcData(
                                    gameObject.name, 
                                    gameObject.transform.position, 
                                    0, 
                                    SceneManager.GetActiveScene().name
                                );
        }

    }

    private void Start() 
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;    
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;    
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateNpcData();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        UpdateNpcData();
    }

    private void UpdateNpcData()
    {
        npcData.position = gameObject.transform.position;
        npcData.sceneName = SceneManager.GetActiveScene().name;
        NPCManager.Instance.AddOrUpdate(npcData);
        Debug.Log(npcData.name + " AddOrUpdate");
    }
}