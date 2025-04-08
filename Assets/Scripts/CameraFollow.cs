using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float offsetY = 2.5f; // �÷��̾� �Ʒ� ��ġ�� ī�޶� ����� �Ÿ�
    public float smoothSpeed = 5f;

    private float targetY;

    void Start()
    {
        targetY = transform.position.y;
    }

    void LateUpdate()
    {
        // ���θ� �̵�
        if (targetY > transform.position.y)
        {
            float newY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        // �÷��̾ ȭ�� �Ʒ��� ���������� Ȯ��
        float camBottom = transform.position.y - Camera.main.orthographicSize;
        if (player.position.y < camBottom)
        {
            Debug.Log("���� ����!");
            // TODO: ���� ���� ó�� �Լ� ȣ��
        }
    }

    // �÷��̾ �÷����� �������� �� ȣ������ �Լ�
    public void MoveCameraToPlatform(float platformY)
    {
        float desiredY = platformY - offsetY;
        if (desiredY > targetY)
        {
            targetY = desiredY;
        }
    }
}
