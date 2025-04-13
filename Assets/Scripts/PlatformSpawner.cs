using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;     // 발판 프리팹
    public int numberOfPlatforms = 5;     // 동시에 유지할 발판 개수
    public float verticalSpacing = 3f;    // 발판 간 Y 간격
    public float minX = -2.5f;            // X 좌표 최소값
    public float maxX = 2.5f;             // X 좌표 최대값
    public Transform player;

    private GameObject[] platforms;
    private float highestY; // 가장 높은 발판 위치

    void Start()
    {
        platforms = new GameObject[numberOfPlatforms];
        highestY = player.position.y;

        // 처음 발판들 배치
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            float yPos = i * verticalSpacing;
            float xPos = Random.Range(minX, maxX);
            Vector3 spawnPos = new Vector3(xPos, yPos, 0);
            platforms[i] = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
            highestY = Mathf.Max(highestY, yPos);
        }
    }

    void Update()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            // 플레이어가 일정 높이 이상 올라가면, 아래 발판을 위로 이동시키기
            if (player.position.y > platforms[i].transform.position.y + verticalSpacing * 2f)
            {
                float newY = highestY + verticalSpacing;
                float newX = Random.Range(minX, maxX);
                platforms[i].transform.position = new Vector3(newX, newY, 0);
                highestY = newY;
            }
        }
    }
}
