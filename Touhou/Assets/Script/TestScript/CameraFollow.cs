using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;   // 카메라와 플레이어 사이의 거리
    public GameObject playerObject;

    private void LateUpdate()
    {
        // "Player"라는 이름을 가진 게임 오브젝트를 찾아서 할당
        playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            Transform playerTransform = playerObject.transform;
            transform.position = playerTransform.position + offset;
        }
    }
}