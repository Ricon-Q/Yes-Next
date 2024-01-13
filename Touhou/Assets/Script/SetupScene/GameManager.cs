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
        // InventoryManager.Instance.IsActive();
        InputManager.Instance.IsActive();
        ShopManager.Instance.IsActive();
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
            DataManager.Instance.LoadSlot(DataManager.Instance.currentSaveIndex);
            
            // 불러온 데이터 적용
            _PlayerManager.Instance.TogglePlayer(true);

            // Area 설정
            CameraManager.Instance.ChangeCameraBorder(_PlayerManager.Instance.playerData.currentArea);
            
            // 인벤토리 정보창 업데이트
            // InventoryManager.Instance.UpdateCharacterInfo();

            // 플레이어 Input모드 변경
            PlayerInputManager.Instance.SetInputMode(true);
            DataManager.Instance.LoadInventory(DataManager.Instance.currentSaveIndex);
        
            // 씬 불러오기
            FadeInOutManager.Instance.ChangeScene(DataManager.Instance.loadData.LastSceneName, DataManager.Instance.loadData.playerPosition);
        }
        else
        {
            // 저장 데이터가 없을 경우

            // 인트로 씬으로 이동
            FadeInOutManager.Instance.ChangeScene("Intro Scene");
            
            // Player 비활성화 (컷신을 위해서)
            _PlayerManager.Instance.TogglePlayer(false);
            // UI 비활성화 (컷신을 위해서)
            UiManager.Instance.ToggleUiCanvas();
            
            // Player Camera 비활성화 (컷신을 위해서)
            CameraManager.Instance.TogglePlayerCamera(false);
        }
        
        yield return null;
    }

    // private IEnumerator GameReady()
    // {
    //     Debug.Log("Ready");
    //     yield return null;
    // }
}
