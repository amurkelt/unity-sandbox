using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int FoundObjects { get; private set; }

    [SerializeField] private string levelName;
    [SerializeField] private MenuManager menuManager;

    private string levelProgress;
    private string lastTime;

    private HiddenObject[] hiddenObjectsCache;
    private int totalObjects;

    private void Reset()
    {
        levelName = SceneManager.GetActiveScene().name;
        menuManager = FindFirstObjectByType<MenuManager>();
    }

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

        hiddenObjectsCache = FindObjectsByType<HiddenObject>(FindObjectsSortMode.None);
        totalObjects = hiddenObjectsCache.Length;
    }

    private void Start()
    {
        levelProgress = levelName + "Progress";
        lastTime = levelName + "LastTime";

        FoundObjects = PlayerPrefs.GetInt(levelProgress, 0);
        UpdateCountText();

        float savedTime = PlayerPrefs.GetFloat(lastTime, 0f);
        UpdateTimeText(savedTime);

        if (IsLevelCompleted)
        {
            LevelTimer.Instance.StopTimer();
        }
        else
        {
            LevelTimer.Instance.StartTimer(savedTime);
        }
    }

    public void AddScore(int amount)
    {
        FoundObjects += amount;
        UpdateCountText();

        if (IsLevelCompleted)
            CompleteLevel();

        PlayerPrefs.SetInt(levelProgress, FoundObjects);
        PlayerPrefs.Save();
    }

    public void ResetScore()
    {
        FoundObjects = 0;

        foreach (var obj in hiddenObjectsCache)
        {
            obj.ResetFound();
        }

        //UpdateCountText();
        //UpdateTimeText(0f);

        PlayerPrefs.SetInt(levelProgress, 0);
        PlayerPrefs.SetFloat(lastTime, 0.0f);
        PlayerPrefs.Save();
    }

    public void SaveElapsedTime()
    {
        float time = LevelTimer.Instance.GetTime();
        PlayerPrefs.SetFloat(lastTime, time);
        Debug.LogError($"time {time}");
        PlayerPrefs.Save();
    }

    private void UpdateCountText()
    {
        HUDManager.Instance.UpdateCounterText(FoundObjects, totalObjects);
    }

    private void UpdateTimeText(float elapsedTime)
    {
        HUDManager.Instance.UpdateTimerText(elapsedTime);
        Debug.LogError($"time {elapsedTime}");
    }

    public bool IsLevelCompleted => FoundObjects >= totalObjects;

    private void CompleteLevel()
    {
        LevelTimer.Instance.StopTimer();
        SaveElapsedTime();

        menuManager.ShowLevelComplete();
    }
}
