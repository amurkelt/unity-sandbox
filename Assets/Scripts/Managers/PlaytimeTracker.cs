using UnityEngine;

public class PlaytimeTracker : MonoBehaviour
{
    private float sessionTime;

    private void Update()
    {
        sessionTime += Time.unscaledDeltaTime;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        var totalPlayTime = PlayerPrefs.GetFloat("PlayTime", 0);
        totalPlayTime += sessionTime;

        PlayerPrefs.SetFloat("PlayTime", totalPlayTime);
        //PlayerPrefs.Save();

        sessionTime = 0f;
    }
}