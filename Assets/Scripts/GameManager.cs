using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int totalBlocks;  // 全ブロック数
    private int destroyedBlocks = 0;
    [SerializeField] private string goSceneName;//ゲームオーバー時の遷移先
    [SerializeField] private string clSceneName;//クリア時の遷移先

    void Start()
    {
        // シーン内にあるBlockの数をカウント
        totalBlocks = GameObject.FindGameObjectsWithTag("Block").Length;
    }

    public void OnBlockDestroyed()
    {
        destroyedBlocks++;

        // 全部壊したらクリア！
        if (destroyedBlocks >= totalBlocks)
        {
            Invoke(nameof(GoClearScene), 1f); // 1秒後にクリア画面へ
        }
    }

    public void OnBallLost()
    {
        Invoke(nameof(GoGameOverScene), 1f); // ボール落下でゲームオーバー
    }

    private void GoClearScene()
    {
        SceneManager.LoadScene(clSceneName);
    }

    private void GoGameOverScene()
    {
        SceneManager.LoadScene(goSceneName);
    }
}
