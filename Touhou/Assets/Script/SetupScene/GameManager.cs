using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
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

    /*/
    Managers
        Inventory Manager
        Input Manager
        Shop Manager
        Data Manager (Mainmenu Scene)
        EventSystemManager

    Camera
        Camera Manager
    Player
        Player Manager
    /*/

    /*/
    1. 게임 매니저 활성화
    2. 각종 매니저 활성화
    3. 매니저 활성화 체크
    4. Data Manager로 데이터 불러오기
    5. 자동으로 씬 불러오기
    /*/

    // [SerializeField] private GameObject playerObject;

    private void Start()
    {
        StartCoroutine(IEnum_ManagerCheck());
    }

    IEnumerator IEnum_ManagerCheck()
    {
        yield return StartCoroutine(ManagerCheck());

        StartCoroutine(IEnum_DataLoad());
    }

    IEnumerator IEnum_DataLoad()
    {
        yield return StartCoroutine(DataLoad());
        // StartCoroutine(IEnum_GameReady());
    }

    // IEnumerator IEnum_GameReady()
    // {
    //     yield return StartCoroutine(GameReady());
    // }

    private IEnumerator ManagerCheck()
    {
        InventoryManager.Instance.IsActive();
        InputManager.Instance.IsActive();
      // ShopManager.Instance.IsActive();
        DataManager.Instance.IsActive();
        // EventSystemManager.Instance.IsActive();
        CameraManager.Instance.IsActive();
        _PlayerManager.Instance.IsActive();

        yield return null;
    }

    private IEnumerator DataLoad()
    {
        if(DataManager.Instance.CheckSaveSlot())
        {            
            // 저장 데이터가 있을 경우
                // 게임 데이터 불러오기 
                // 씬 불러오기
            _PlayerManager.Instance.TogglePlayer(false);
            DataManager.Instance.LoadSlot(DataManager.Instance.currentSaveIndex);
        }
        else
        {
            // 저장 데이터가 없을 경우
                // Player 비활성화 (컷신을 위해서)
                // 인트로 씬으로 이동
            // playerObject.SetActive(false);
            _PlayerManager.Instance.TogglePlayer(false);
            FadeInOutManager.Instance.ChangeScene("Intro Scene");
        }
        
        yield return null;
    }

    // private IEnumerator GameReady()
    // {
    //     Debug.Log("Ready");
    //     yield return null;
    // }
}
