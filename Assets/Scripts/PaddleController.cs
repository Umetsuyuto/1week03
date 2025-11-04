using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // 移動速度
    [SerializeField] private float limitX = 8f;     // 画面端の制限

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // 物理影響は受けない
    }

    void Update()
    {
        // A/Dキー または ←→キー で移動
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 移動先計算
        Vector2 newPos = rb.position + Vector2.right * moveInput * moveSpeed * Time.deltaTime;

        // 画面端でストップ
        newPos.x = Mathf.Clamp(newPos.x, -limitX, limitX);

        // Rigidbodyで移動（スムーズ）
        rb.MovePosition(newPos);
    }
}
