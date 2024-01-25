using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HotBarSelected : MonoBehaviour
{
    private Vector2 _mouseScroll = Vector2.zero;
    [SerializeField] private int _hotBarSelected = 0;
    public List<GameObject> _hotBarItems = new List<GameObject>();
    public RectTransform _image;

    private void Update() 
    {
        ChangeHotBar();
        MoveImage();
    }

    public void ChangeHotBar()
    {
        if(InputManager.Instance.GetMouseScroll().y > 0)
        {
            _hotBarSelected = Mathf.Clamp(_hotBarSelected+1, 0, 11);
        }
        else if(InputManager.Instance.GetMouseScroll().y < 0)
        {
            _hotBarSelected = Mathf.Clamp(_hotBarSelected-1, 0, 11);
        }
    }

    public void MoveImage()
    {
        _image.anchoredPosition  = new Vector3(_hotBarSelected * 100, 0, 0); 
    }
}
