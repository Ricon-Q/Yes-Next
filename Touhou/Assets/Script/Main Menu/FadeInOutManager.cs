using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeInOutManager : MonoBehaviour
{
    private static FadeInOutManager instance;
 
    public static FadeInOutManager Instance
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
        else
        {
            Destroy(this.gameObject);
        }
    }


    public DOTweenAnimation anim;
    public GameObject FadeImage;

    private void Start() 
    {
        FadeImage.SetActive(false);
    }
    public void FadeIn()
    {
        FadeImage.SetActive(true);
        anim.DOPlayById("In");
    }

    public void FadeOut()
    {
        anim.DOPlayById("Out");
        FadeImage.SetActive(false);
    }
}
