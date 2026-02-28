using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string levelScore;

    public int CurrentScore { get; private set; }

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

        HUDManager.Instance.UpdateText(CurrentScore);
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        HUDManager.Instance.UpdateText(CurrentScore);

        PlayerPrefs.SetInt(levelScore, CurrentScore);
    }
}
