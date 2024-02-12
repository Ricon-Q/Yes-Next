using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        itemDatabaseObject.UpdateItemDatabase();
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

    // ======================================================= //

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
    [SerializeField] private ItemDatabaseObject itemDatabaseObject;

     private void Start()
    {
        StartCoroutine(IEnum_ManagerCheck());
    }

    IEnumerator IEnum_ManagerCheck()
    {
        yield return StartCoroutine(ManagerCheck());
        
        StartCoroutine(IEnum_DataLoad());
    }
    private IEnumerator ManagerCheck()
    {
        // InventoryManager.Instance.IsActive();
        InputManager.Instance.IsActive();
        // ShopManager.Instance.IsActive();
        DataManager.Instance.IsActive();
        // EventSystemManager.Instance.IsActive();
        CameraManager.Instance.IsActive();
        _PlayerManager.Instance.IsActive();

        yield return null;
    }
    IEnumerator IEnum_DataLoad()
    {
        yield return StartCoroutine(DataLoad());
        // StartCoroutine(IEnum_GameReady());
    }

    private IEnumerator DataLoad()
    {
        if(DataManager.Instance.CheckSaveSlot())
        {            
            // 저장 데이터가 있을 경우 // 

            // 게임 데이터 불러오기 
            DataManager.Instance.LoadSlot(DataManager.Instance.currentSaveIndex);
            
            // 불러온 데이터 적용
            _PlayerManager.Instance.TogglePlayer(true);

            // Area 설정
            CameraManager.Instance.ChangeCameraBorder(_PlayerManager.Instance.playerData.currentArea);
            
            // 인벤토리 정보창 업데이트
            // InventoryManager.Instance.UpdateCharacterInfo();

            // 플레이어 Input모드 변경
            // PlayerInputManager.Instance.SetInputMode(true);
            PlayerInputManager.SetPlayerInput(true);
            
            // 인벤토리 불러오기 및 핫바 새로고침
            DataManager.Instance.LoadInventory(DataManager.Instance.currentSaveIndex);
            UiManager.Instance.inventoryHotBarDisplay.RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);
    
            // 퀘스트 게시판 불러오기 Trigger
            QuestManager.Instance._guildQuestDisplay._haveLoadData = true;

            // 씬 불러오기
            FadeInOutManager.Instance.ChangeScene(DataManager.Instance.loadData.LastSceneName, DataManager.Instance.loadData.playerPosition);
        }
        else
        {
            // 저장 데이터가 없을 경우

            // 인트로 씬으로 이동
            FadeInOutManager.Instance.ChangeScene("Intro Scene", default, false);
            // 인벤토리 설정
            PlayerInventoryManager.Instance.GeneratePlayerInventory();
            
            // Player 비활성화 (컷신을 위해서)
            _PlayerManager.Instance.TogglePlayer(false);
            // UI 비활성화 (컷신을 위해서)
            UiManager.Instance.ToggleUiCanvas(false);
            
            // Player Camera 비활성화 (컷신을 위해서)
            CameraManager.Instance.TogglePlayerCamera(false);

            StartNewDay();
        }
        
        yield return null;
    }

    public void DisableAllManager()
    {
        // 실행중인 모든 매니저 종료(ShopManager, CraftManager, Inventory)
        _ShopManager.Instance.ExitShopMode();
        _CraftManager.Instance.ExitCraftMode();
        if(PlayerInventoryManager.Instance.isInventoryOpen)
            PlayerInventoryManager.Instance.ToggleInventory();
    }

    public void MoveWithFade(string _sceneName, Vector3 _playerPosition, string _areaName, Vector3 _cameraPosition, int _targetHour)
    {
        StartCoroutine(IEnum_MoveWithFade(_sceneName, _playerPosition, _areaName, _cameraPosition, _targetHour));
    }

    public IEnumerator IEnum_MoveWithFade(string _sceneName, Vector3 _playerPosition, string _areaName, Vector3 _cameraPosition, int _targetHour)
    {
        FadeInOutManager.Instance.FadeOut();

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(_sceneName);
        _PlayerManager.Instance.transform.position = _playerPosition;
        
        _PlayerManager.Instance.playerData.currentArea = _areaName;
        CameraManager.Instance.ChangeCameraBorder(_areaName);
        CameraManager.Instance.transform.position = _cameraPosition;

        _TimeManager.Instance.SetTargetTimeHour(_targetHour);

        yield return null;

        // yield return new WaitForEndOfFrame();
        FadeInOutManager.Instance.FadeIn();
    }

    public void StartNewDay()
    {
        // 하루가 지날때 해당 함수 호출, 침대에서 취침하거나 기절하여 하루가 지날때 호출

        // Day가 7의 배수라면 퀘스트 새로고침
        if(_TimeManager.Instance.timeData.day % 7 == 1)
        {
            QuestManager.Instance._guildQuestDisplay._isQeustRefresh = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    [Header("Back To Title Object")]
    // GameManager
    [SerializeField] private GameObject _gameManager;
    // Player
    [SerializeField] private GameObject _player;
    // DataManager
    // FadeInOutManager
    // DialogueManager
    [SerializeField] private GameObject _dialogueManager;

    public void BackToTitle()
    {
        // Dont Destroy 오브젝트들 전부 삭제
        // MainMenu씬으로 이동
        Destroy(_player);
        Destroy(_dialogueManager);
        SceneManager.LoadScene("Main Menu");
        Destroy(_gameManager);
    }
}
