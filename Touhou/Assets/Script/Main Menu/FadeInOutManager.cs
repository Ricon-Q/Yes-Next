using System.Collections;
using System.Collections.Generic;
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

    public GameObject fadeInOutCanvas;
    public DOTweenAnimation fadeInOutAnimation;
    
    private void Start() 
    {
        fadeInOutCanvas.SetActive(false);    
    }

    public IEnumerator _ChangeScene(string sceneName)
    {
        // Debug.Log("Changing to Scene: " + sceneName);
        FadeOut();
        yield return new WaitForSeconds(2);
        
        // 비동기적으로 씬을 로드합니다.
        yield return SceneManager.LoadSceneAsync(sceneName);
        
        // 로드가 완료된 후에 페이드 아웃을 시작합니다.
        FadeIn();
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(_ChangeScene(sceneName));
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
