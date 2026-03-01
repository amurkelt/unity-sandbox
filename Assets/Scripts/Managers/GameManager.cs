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
    private readonly int totalObjects = 100;

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
    }

    private void Start()
    {
        levelProgress = levelName + "Progress";
        lastTime = levelName + "LastTime";

        FoundObjects = PlayerPrefs.GetInt(levelProgress, 0);

        UpdateCountText();

        if (IsLevelCompleted())
        {
            UpdateTimeText(PlayerPrefs.GetFloat(lastTime, 0));
        }
        else
        {
            LevelTimer.Instance.StartTimer();
        }

        hiddenObjectsCache = FindObjectsByType<HiddenObject>(FindObjectsSortMode.None);
    }

    public void AddScore(int amount)
    {
        FoundObjects += amount;
        UpdateCountText();

        if (IsLevelCompleted())
            CompleteLevel();

        PlayerPrefs.SetInt(levelProgress, FoundObjects);
    }

    public void ResetScore()
    {
        FoundObjects = 0;

        foreach (var obj in hiddenObjectsCache)
        {
            obj.ResetFound();
        }

        //UpdateCountText();

        PlayerPrefs.SetInt(levelProgress, 0);
        PlayerPrefs.SetFloat(lastTime, 0.0f);
    }

    private void UpdateCountText()
    {
        HUDManager.Instance.UpdateCounterText(FoundObjects);
    }

    private void UpdateTimeText(float elapsedTime)
    {
        HUDManager.Instance.UpdateTimerText(elapsedTime);
    }

    private bool IsLevelCompleted()
    {
        if (FoundObjects >= totalObjects)
        {
            return true;
        }
        else { return false; }
    }

    private void CompleteLevel()
    {
        LevelTimer.Instance.StopTimer();

        float time = LevelTimer.Instance.GetTime();
        PlayerPrefs.SetFloat(lastTime, time);

        PlayerPrefs.Save();

        menuManager.ShowLevelComplete();
    }
}
