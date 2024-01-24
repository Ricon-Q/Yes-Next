using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

using ES3Types;


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
        // ES3Type.AddType(typeof(InventoryItemData), ES3UserType_InventoryItemData.Instance);
        fadeInOutCanvas.SetActive(false);    
    }

    public IEnumerator IEnum_ChangeScene(string sceneName, Vector3 spawnPoint = default(Vector3))
    {
        // Debug.Log("Changing to Scene: " + sceneName);
        FadeOutZero();
        _PlayerManager.Instance.transform.position = spawnPoint;
        yield return new WaitForSeconds(1f);
        
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

    public void FadeInOut(int time)
    {
        StartCoroutine(IEnum_FadeInOut(time));
    }

    public IEnumerator IEnum_FadeInOut(int time)
    {
        FadeOut();
        yield return new WaitForSeconds(time);
        FadeIn();
    }

    public void FadeIn()
    {
        // Debug.Log("In");
        fadeInOutAnimation.DOPlayById("In");
        
        PlayerInputManager.Instance.SetInputMode(true);

        return;
    }

    public void FadeOut()
    {
        Debug.Log("Out");
        PlayerInputManager.Instance.SetInputMode(false);
        fadeInOutCanvas.SetActive(true);
        fadeInOutAnimation.DOPlayById("Out");

        return;
    }

    public void FadeOutZero()
    {
        // Debug.Log("Out");
        fadeInOutCanvas.SetActive(true);
        fadeInOutAnimation.DOPlayById("OutZero");
    }
    // public void ChangeDuration(float timeToFade)
    // {
    //     fadeInOutAnimation.DO
    // }
}


