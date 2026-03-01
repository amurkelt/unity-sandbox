using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance;

    private float elapsed;
    private bool running;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!running) return;

        if (!PauseManager.Instance.IsPaused)
            elapsed += Time.unscaledDeltaTime;

        UpdateTimeText();
    }

    public void StartTimer(float startTime = 0f)
    {
        elapsed = startTime;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public float GetTime()
    {
        return elapsed;
    }

    private void UpdateTimeText()
    {
        HUDManager.Instance.UpdateTimerText(GetTime());
    }
}