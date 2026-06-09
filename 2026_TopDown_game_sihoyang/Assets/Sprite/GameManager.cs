using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string titleSceneName = "TitleScene";
    public string gameSceneName = "GameScene";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void GameOver()
    {
        PlayerController player =
            FindFirstObjectByType<PlayerController>();

        GameDataManager.Instance
            .SaveGameResult(player.bulletCount);

        GoTitle();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}
