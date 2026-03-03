using UnityEngine;

public class PlaytimeTracker : MonoBehaviour
{
    private double sessionTime;

    public static double TotalPlayTime
    {
        get => SaveManager.GetTotalPlaytime();
    }

    private void Update()
    {
        sessionTime += Time.unscaledDeltaTime;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        if (sessionTime <= 0)
            return;

        SaveManager.AddPlaytime(sessionTime);
        sessionTime = 0;

        AchievementManager.Instance.CheckAll();
    }
}