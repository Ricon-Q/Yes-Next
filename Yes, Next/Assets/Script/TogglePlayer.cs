using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _PlayerManager.Instance.TogglePlayer(true);
        PlayerInputManager.Instance.SetInputMode(true);
    }
}
