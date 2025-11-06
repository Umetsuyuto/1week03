using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;          // ボールの速度
    [SerializeField] private float reflectCooldown = 0.05f; // 反射クールタイム
    [SerializeField] private AudioSource audioSource;   // 反射SE

    private Rigidbody2D rb;
    private bool isLaunched = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // スペースで発射
        if (!isLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    private void Launch()
    {
        isLaunched = true;
        Vector2 dir = new Vector2(Random.Range(-0.6f, 0.6f), 1).normalized; // ちょっとランダム
        rb.linearVelocity = dir * speed;
    }

    private void FixedUpdate()
    {
        if (isLaunched && rb.linearVelocity.magnitude != speed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (cooldownTimer > 0f) return; // クールタイム中は無視

        // 🔊 Paddleに当たったら音再生
        if (collision.gameObject.CompareTag("Paddle"))
        {
            audioSource.Play();
        }

        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectDir = Vector2.Reflect(rb.linearVelocity.normalized, normal);

        // 水平・垂直すぎる反射を防ぐ
        if (Mathf.Abs(reflectDir.x) < 0.3f)
            reflectDir.x = Mathf.Sign(reflectDir.x) * 0.3f;
        if (Mathf.Abs(reflectDir.y) < 0.3f)
            reflectDir.y = Mathf.Sign(reflectDir.y) * 0.3f;

        rb.linearVelocity = reflectDir.normalized * speed;

        // めり込み防止
        transform.position += (Vector3)normal * 0.05f;

        cooldownTimer = reflectCooldown;
    }
}
