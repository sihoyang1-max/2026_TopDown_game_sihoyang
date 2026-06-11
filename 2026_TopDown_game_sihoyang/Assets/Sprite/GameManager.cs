using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string titleSceneName = "TitleScene";
    public string gameSceneName = "GameScene";
    public TextMeshProUGUI timerText;

    public float startTime = 60f;
    public float surviveTime;
    private bool gameEnded = false;
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
        Time.timeScale = 1f;

        surviveTime = startTime;

        Debug.Log("시작 시간 : " + startTime);

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
        Time.timeScale = 1f;

        SceneManager.LoadScene(titleSceneName);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != gameSceneName)
            return;

        if (timerText == null)
            return;

        surviveTime -= Time.deltaTime;

        int minutes =
            Mathf.FloorToInt(surviveTime / 60);

        int seconds =
            Mathf.FloorToInt(surviveTime % 60);

        timerText.text =
            string.Format("{0:00}:{1:00}",
            minutes,
            seconds);

        if (surviveTime <= 0)
        {
            WinGame();
        }
    }
    public void WinGame()
    {
        gameEnded = true;

        timerText.text = "탈출 성공!";

        PlayerController player =
            FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            player.canMove = false;
        }

        StartCoroutine(WinRoutine());
        EnemyAI[] enemies =
    FindObjectsByType<EnemyAI>(
        FindObjectsSortMode.None);

        foreach (EnemyAI enemy in enemies)
        {
            enemy.canMove = false;
        }
    }
    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(3f);

        GoTitle();
    }
}
