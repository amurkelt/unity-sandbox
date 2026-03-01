using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int FoundObjects { get; private set; }

    [SerializeField] private string levelName;
    public string LevelName => levelName;
    [SerializeField] private MenuManager menuManager;

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
        var levelData = SaveManager.GetLevel(levelName);

        FoundObjects = levelData.objectsFound;
        UpdateCountText();

        UpdateTimeText(levelData.bestTime);

        if (IsLevelCompleted)
            LevelTimer.Instance.StopTimer();
        else
            LevelTimer.Instance.StartTimer(levelData.bestTime);
    }

    public void AddScore(int amount)
    {
        FoundObjects += amount;
        UpdateCountText();

        var levelData = SaveManager.GetLevel(levelName);
        levelData.objectsFound = FoundObjects;
        SaveManager.SaveLevel(levelData);

        if (IsLevelCompleted)
            CompleteLevel();
    }

    public void ResetScore()
    {
        FoundObjects = 0;

        var levelData = SaveManager.GetLevel(levelName);
        levelData.objectsFound = 0;
        levelData.bestTime = 0;
        levelData.foundObjectIds.Clear();

        SaveManager.SaveLevel(levelData);
    }

    public void SaveElapsedTime()
    {
        float time = LevelTimer.Instance.GetTime();

        var levelData = SaveManager.GetLevel(levelName);

        if (levelData.bestTime == 0 || time < levelData.bestTime)
            levelData.bestTime = time;

        SaveManager.SaveLevel(levelData);
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
