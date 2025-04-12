using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float offsetY = 2.5f; // 플레이어 아래 위치로 카메라가 따라올 거리
    public float smoothSpeed = 5f;

    private float targetY;

    void Start()
    {
        targetY = transform.position.y;
    }

    void LateUpdate()
    {
        // 위로만 이동
        if (targetY > transform.position.y)
        {
            float newY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        // 플레이어가 화면 아래로 떨어졌는지 확인
        float camBottom = transform.position.y - Camera.main.orthographicSize;
        if (player.position.y < camBottom)
        {
            Debug.Log("게임 오버!");
            // TODO: 게임 오버 처리 함수 호출
        }
    }

    // 플레이어가 플랫폼에 착지했을 때 호출해줄 함수
    public void MoveCameraToPlatform(float platformY)
    {
        float desiredY = platformY - offsetY;
        if (desiredY > targetY)
        {
            targetY = desiredY;
        }
    }
}
