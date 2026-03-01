#if UNITY_PS5 && !UNITY_EDITOR
using UnityEngine;
using UnityEngine.PS5; // PS5 APIs

public class PS5SaveHandler : MonoBehaviour
{
    private bool _wasSaved = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveManager.Save();
        Debug.Log("PS5 Save on focus");
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveManager.Save();
        Debug.Log("PS5 Save on pause");
    }

    //private void Update()
    //{
    //    // Optional: background execution / system UI overlay
    //    if (!_wasSaved && (Utility.isInBackgroundExecution || Utility.isSystemUiOverlaid))
    //    {
    //        SaveManager.Save();
    //        _wasSaved = true;
    //        Debug.Log("PS5 Save triggered in background");
    //    }
    //    else if (_wasSaved && !Utility.isInBackgroundExecution && !Utility.isSystemUiOverlaid)
    //    {
    //        _wasSaved = false;
    //    }
    //}
}
#endif