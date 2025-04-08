using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool scored = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!scored && collision.gameObject.CompareTag("Player"))
        {
            scored = true;
            ScoreManager.Instance.AddScore(1);
        }
    }
}
