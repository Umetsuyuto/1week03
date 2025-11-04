using UnityEngine;
using TMPro;

public class StartTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private GameObject ball; // ボールの参照
    private BallController ballController;

    void Start()
    {
        ballController = ball.GetComponent<BallController>();
        startText.gameObject.SetActive(true); // 最初は表示
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ボール発射されたら非表示
            startText.gameObject.SetActive(false);
        }
    }
}
