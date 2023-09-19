// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class PlantGrowth : MonoBehaviour
// {
//     public SpriteRenderer spriteRenderer;
//     public Sprite[] growthStages; // 스프라이트 배열, 0부터 순서대로 성장

//     private TimeManager timeManager;
//     private int currentStage = 0;

//     private string spawnSceneName;

//     private void Start()
//     {
//         timeManager = TimeManager.Instance;

//         // 처음 스폰된 씬에서만 활성화
//         spawnSceneName = SceneManager.GetActiveScene().name;
//         gameObject.SetActive(SceneManager.GetActiveScene().name == spawnSceneName);

//         if (gameObject.activeSelf)
//         {
//             spriteRenderer.sprite = growthStages[currentStage];
//             DontDestroyOnLoad(gameObject);
//         }
//     }
//     private void Update()
//     {
//         // 날짜 변화를 감지하고 스프라이트 변경
//         if (timeManager != null && currentStage < growthStages.Length - 1)
//         {
//             if (timeManager.timeData.day > currentStage + 1)
//             {
//                 currentStage = Mathf.Min(timeManager.timeData.day - 1, growthStages.Length - 1);
//                 spriteRenderer.sprite = growthStages[currentStage];
//             }
//         }
//     }

//     // 다른 씬으로 이동할 때 호출되는 함수
//     private void OnDisable()
//     {
//         // 활성화된 경우, 비활성화하고 씬을 이동
//         if (gameObject.activeSelf)
//         {
//             gameObject.SetActive(false);
//             UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
//         }
//     }

//     // 처음 스폰된 씬으로 돌아올 때 호출되는 함수
//     private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
//     {
//         if (scene.name == spawnSceneName)
//         {
//             gameObject.SetActive(true);
//             UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
//         }
//     }
// }