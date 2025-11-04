using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChengeClick : MonoBehaviour
{
    [SerializeField] private string nextScene;
    public void Onclick()
    {
        SceneManager.LoadScene(nextScene);
    }
}
