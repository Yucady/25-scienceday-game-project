using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;     // ���� ������
    public int numberOfPlatforms = 5;     // ���ÿ� ������ ���� ����
    public float verticalSpacing = 3f;    // ���� �� Y ����
    public float minX = -2.5f;            // X ��ǥ �ּҰ�
    public float maxX = 2.5f;             // X ��ǥ �ִ밪
    public Transform player;

    private GameObject[] platforms;
    private float highestY; // ���� ���� ���� ��ġ

    void Start()
    {
        platforms = new GameObject[numberOfPlatforms];
        highestY = player.position.y;

        // ó�� ���ǵ� ��ġ
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
            // �÷��̾ ���� ���� �̻� �ö󰡸�, �Ʒ� ������ ���� �̵���Ű��
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
