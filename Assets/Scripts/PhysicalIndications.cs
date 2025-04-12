using UnityEngine;
using TMPro;

public class PhysicalIndications : MonoBehaviour
{
    [Header("플레이어 Rigidbody2D")]
    public Rigidbody2D playerRigidbody;

    [Header("UI 텍스트")]
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI gravityText;

    private void Update()
    {
        if (playerRigidbody == null) return;

        // 속도 계산
        Vector2 velocity = playerRigidbody.linearVelocity;
        float speed = velocity.magnitude;

        // 중력 가속도
        Vector2 gravity = Physics2D.gravity;

        // UI에 표시
        velocityText.text = $"Velocity: {speed:F2} m/s";
        gravityText.text = $"Gravity: {gravity.y:F2} m/s²";
    }
}
