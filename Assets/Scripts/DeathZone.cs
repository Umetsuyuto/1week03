using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            FindAnyObjectByType<GameManager>().OnBallLost();
            Destroy(collision.gameObject); // É{Å[ÉãçÌèú
        }
    }
}