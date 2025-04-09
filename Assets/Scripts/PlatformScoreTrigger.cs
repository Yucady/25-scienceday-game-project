using UnityEngine;

public class PlatformScoreTrigger : MonoBehaviour
{
    private bool hasScored = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasScored) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.GetContact(0);

            if (contact.normal.y > 0.5f)
            {
                hasScored = true;
                ScoreManager.Instance?.AddScore(1);
                Camera.main.GetComponent<CameraFollow>()?.MoveCameraToPlatform(transform.position.y);
            }
        }
    }
}
