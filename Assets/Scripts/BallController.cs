using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Rigidbody2D rb;
    private bool isLaunched = false;
    private float reflectCooldown = 0f; // 連続反射防止用

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!isLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        if (reflectCooldown > 0)
            reflectCooldown -= Time.deltaTime;
    }

    private void Launch()
    {
        isLaunched = true;
        Vector2 dir = new Vector2(Random.Range(-0.6f, 0.6f), 1).normalized;
        rb.linearVelocity = dir * speed;
    }

    private void FixedUpdate()
    {
        if (isLaunched)
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (reflectCooldown > 0f) return; // 連続反射防止

        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectDir = Vector2.Reflect(rb.linearVelocity.normalized, normal);

        rb.linearVelocity = reflectDir * speed;

        // めり込み防止
        transform.position += (Vector3)normal * 0.1f;

        reflectCooldown = 0.05f; // ほんの少しだけクールタイム
    }
}
