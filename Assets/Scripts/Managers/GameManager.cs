using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CurrentScore { get; private set; }
    private string levelScore;
    private HiddenObject[] hiddenObjectsCache;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name.ToString();
        levelScore = sceneName + "Score";

        CurrentScore = PlayerPrefs.GetInt(levelScore, 0);

        UpdateScoreText();

        hiddenObjectsCache = FindObjectsByType<HiddenObject>(FindObjectsSortMode.None);
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        UpdateScoreText();

        PlayerPrefs.SetInt(levelScore, CurrentScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;

        foreach (var obj in hiddenObjectsCache)
        {
            obj.ResetFound();
        }

        UpdateScoreText();

        PlayerPrefs.SetInt(levelScore, 0);
    }

    private void UpdateScoreText()
    {
        HUDManager.Instance.UpdateText(CurrentScore);
    }
}
