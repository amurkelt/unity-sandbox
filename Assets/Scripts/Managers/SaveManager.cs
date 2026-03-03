using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public List<LevelSaveData> levels = new();
    public SettingsSaveData settings = new();
    public List<string> unlockedAchievements = new();
    public double totalPlaytime;
}

[Serializable]
public class LevelSaveData
{
    public string levelName;
    public int objectsFound;
    public int totalObjects;
    public float bestTime;
    public List<string> foundObjectIds = new();

    public LevelSaveData(string name)
    {
        levelName = name;
        objectsFound = 0;
        totalObjects = 0;
        bestTime = 0f;
    }
}

[Serializable]
public class SettingsSaveData
{
    public float masterVolume = 1f;
    public float effectsVolume = 1f;
    public float mouseSensitivity = 1f;
}

public static class SaveManager
{
    private const string SAVE_KEY = "GAME_SAVE_DATA";
    private static GameSaveData _data;

    #region Initialization

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Load();
    }

    private static void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            _data = JsonUtility.FromJson<GameSaveData>(
                PlayerPrefs.GetString(SAVE_KEY));

            if (_data == null)
                _data = new GameSaveData();
        }
        else
        {
            _data = new GameSaveData();
            Save();
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(_data));
        PlayerPrefs.Save();
    }

    #endregion

    #region LEVELS

    public static LevelSaveData GetLevel(string levelName)
    {
        var level = _data.levels.FirstOrDefault(l => l.levelName == levelName);

        if (level == null)
        {
            level = new LevelSaveData(levelName);
            _data.levels.Add(level);
            Save();
        }

        return level;
    }

    public static List<LevelSaveData> GetAllLevels()
    {
        return _data.levels;
    }

    public static void SaveLevel(LevelSaveData level)
    {
        Save();
    }

    #endregion

    #region SETTINGS

    public static SettingsSaveData GetSettings()
    {
        return _data.settings;
    }

    public static void SaveSettings()
    {
        Save();
    }

    #endregion

    #region ACHIEVEMENTS

    public static void UnlockAchievement(string id)
    {
        if (!_data.unlockedAchievements.Contains(id))
        {
            _data.unlockedAchievements.Add(id);
            Save();
        }
    }

    public static bool HasAchievement(string id)
    {
        return _data.unlockedAchievements.Contains(id);
    }

    public static List<string> GetUnlockedAchievements()
    {
        return _data.unlockedAchievements;
    }

    #endregion

    #region PLAYTIME

    public static double GetTotalPlaytime()
    {
        return _data.totalPlaytime;
    }

    public static void AddPlaytime(double seconds)
    {
        _data.totalPlaytime += seconds;
        Save();
    }

    #endregion
}