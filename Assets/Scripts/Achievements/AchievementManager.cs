using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [SerializeField] private List<AchievementData> achievements;
    [SerializeField] private AchievementPopup popup;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CheckAll();
    }

    public void CheckAll()
    {
        foreach (var achievement in achievements)
        {
            if (SaveManager.HasAchievement(achievement.id))
                continue;

            if (IsUnlocked(achievement))
                Unlock(achievement);
        }
    }

    private bool IsUnlocked(AchievementData data)
    {
        switch (data.type)
        {
            case AchievementType.TotalObjectsFound:
                return GetTotalObjectsFound() >= data.intValue;

            case AchievementType.SpecificObjectId:
                return HasFoundObject(data.stringValue);

            case AchievementType.CompleteLevel:
                return IsLevelCompleted(data.stringValue);

            case AchievementType.CompleteAllObjectsInLevel:
                return IsAllObjectsFound(data.stringValue);

            case AchievementType.LevelUnderTime:
                return IsLevelUnderTime(data.stringValue, data.floatValue);

            case AchievementType.TotalPlaytime:
                return SaveManager.GetTotalPlaytime() >= data.floatValue;

            case AchievementType.CompleteAllLevels:
                return AllLevelsCompleted();

            case AchievementType.CompleteAllAchievements:
                return AllOtherAchievementsUnlocked();

            case AchievementType.CompleteXLevels:
                return CompletedLevelsCount() >= data.intValue;

            default:
                return false;
        }
    }

    private void Unlock(AchievementData data)
    {
        SaveManager.UnlockAchievement(data.id);
        popup.Show(data.title, data.description);
    }

    // ---------------- HELPERS ----------------

    private int GetTotalObjectsFound()
    {
        return SaveManager.GetAllLevels().Sum(l => l.objectsFound);
    }

    private bool HasFoundObject(string objectId)
    {
        return SaveManager.GetAllLevels()
            .Any(l => l.foundObjectIds.Contains(objectId));
    }

    private bool IsLevelCompleted(string levelName)
    {
        var level = SaveManager.GetLevel(levelName);
        return level.totalObjects > 0 &&
               level.objectsFound >= level.totalObjects;
    }

    private bool IsAllObjectsFound(string levelName)
    {
        var level = SaveManager.GetLevel(levelName);
        return level.totalObjects > 0 &&
               level.objectsFound >= level.totalObjects;
    }

    private bool IsLevelUnderTime(string levelName, float time)
    {
        var level = SaveManager.GetLevel(levelName);
        return level.bestTime > 0 && level.bestTime <= time;
    }

    private bool AllLevelsCompleted()
    {
        return SaveManager.GetAllLevels()
            .All(l => l.totalObjects > 0 &&
                      l.objectsFound >= l.totalObjects);
    }

    private bool AllOtherAchievementsUnlocked()
    {
        return achievements
            .Where(a => a.type != AchievementType.CompleteAllAchievements)
            .All(a => SaveManager.HasAchievement(a.id));
    }

    private int CompletedLevelsCount()
    {
        return SaveManager.GetAllLevels()
            .Count(l => l.totalObjects > 0 &&
                        l.objectsFound >= l.totalObjects);
    }
}