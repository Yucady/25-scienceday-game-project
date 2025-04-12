using UnityEngine;
using TMPro;

public class PhysicalIndications : MonoBehaviour
{
    public Rigidbody2D playerRigidbody; // ����� Rigidbody2D ����
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI gravityText;

    private void Update()
    {
        if (playerRigidbody == null) return;

        // ���� �ӵ� ���� (magnitude�� ����ϸ� �ӵ��� ũ��)
        Vector2 velocity = playerRigidbody.linearVelocity;
        float speed = velocity.magnitude;

        // �߷� ���ӵ� (���������� ������ ��)
        Vector2 gravity = Physics2D.gravity;

        // UI�� �ݿ�
        velocityText.text = $"�ӵ�: {speed:F2} m/s";
        gravityText.text = $"�߷� ���ӵ�: {gravity.y:F2} m/s��";
    }
}
