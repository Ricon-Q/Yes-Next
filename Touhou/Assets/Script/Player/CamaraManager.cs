using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private static CameraManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static CameraManager Instance
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

//===========================================================//
    public void IsActive()
    {
        Debug.Log("Camera Manager Is " + gameObject.activeSelf);
    }

    private Vector3 cameraPosition = new Vector3(0, 0, -10);
    [SerializeField] private float cameraMoveSpeed;


    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private Vector2 mapSize;
    private float height;
    private float width; 
    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateCameraPosition();
    }

    public void ChangeCameraBorder(Vector2 center, Vector2 mapSize)
    {   
        this.center = center;
        this.mapSize = mapSize;
    }   
    

    private void UpdateCameraPosition()
    {
        transform.position = Vector3.Lerp(
            transform.position, 
            _PlayerManager.Instance.transform.position + cameraPosition,
            Time.deltaTime * cameraMoveSpeed
            );

        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

     private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
