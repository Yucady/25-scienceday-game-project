using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public GameObject backgroundQuadPrefab;
    public Transform player;
    public float tileHeight = 10f;

    private float lastSpawnY = 0f;
    private List<GameObject> spawnedQuads = new List<GameObject>();

    void Start()
    {
        // �ʱ� Ÿ�� 3�� ���� (�÷��̾� ����)
        for (int i = -1; i <= 2; i++)
        {
            SpawnQuad(i * tileHeight);
        }
        lastSpawnY = 2 * tileHeight;
    }

    void Update()
    {
        if (player.position.y + tileHeight > lastSpawnY)
        {
            SpawnQuad(lastSpawnY);
            lastSpawnY += tileHeight;
        }

        // �ʹ� �Ʒ� �ִ� ��� ����
        for (int i = spawnedQuads.Count - 1; i >= 0; i--)
        {
            if (player.position.y - spawnedQuads[i].transform.position.y > 25f)
            {
                Destroy(spawnedQuads[i]);
                spawnedQuads.RemoveAt(i);
            }
        }
    }

    void SpawnQuad(float y)
    {
        GameObject tile = Instantiate(backgroundQuadPrefab, new Vector3(0, y, 10), Quaternion.identity);
        spawnedQuads.Add(tile);
    }
}
