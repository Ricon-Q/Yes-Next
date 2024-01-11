using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeInOutManager : MonoBehaviour
{
    private static FadeInOutManager instance;

    public static FadeInOutManager Instance
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
            return;
        }
    }

    // ========================================================= //

    public GameObject fadeInOutCanvas;
    public DOTweenAnimation fadeInOutAnimation;
    
    private void Start() 
    {
        fadeInOutCanvas.SetActive(false);    
    }

    public IEnumerator IEnum_ChangeScene(string sceneName, Vector3 spawnPoint = default(Vector3))
    {
        // Debug.Log("Changing to Scene: " + sceneName);
        FadeOut();
        _PlayerManager.Instance.transform.position = spawnPoint;
        yield return new WaitForSeconds(0.3f);
        
        // 비동기적으로 씬을 로드합니다.
        yield return SceneManager.LoadSceneAsync(sceneName);
        // CameraManager.Instance.ChangeCameraBorder(_PlayerManager.Instance.playerData.currentArea);
        
        // 로드가 완료된 후에 페이드 아웃을 시작합니다.
        FadeIn();
    }

    public void ChangeScene(string sceneName, Vector3 spawnPoint = default(Vector3))
    {
        StartCoroutine(IEnum_ChangeScene(sceneName, spawnPoint));
    }

    public void FadeIn()
    {
        // Debug.Log("In");
        fadeInOutAnimation.DOPlayById("In");
    }

    public void FadeOut()
    {
        // Debug.Log("Out");
        fadeInOutCanvas.SetActive(true);
        fadeInOutAnimation.DOPlayById("Out");
    }
    // public void ChangeDuration(float timeToFade)
    // {
    //     fadeInOutAnimation.DO
    // }
}


