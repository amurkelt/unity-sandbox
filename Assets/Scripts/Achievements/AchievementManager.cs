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

        // ----------------- Placeholder actions per achievement -----------------
        switch (data.id)
        {
            case "first_object":
                // TODO: AchievementSystem call
                Debug.Log($"Unlocked achievement {data.id}!");
                break;

            case "find_10":                
                break;

            case "find_25":
                break;

            case "find_50":
                break;

            case "find_75":
                break;

            case "find_300":
                break;

            case "find_400":
                break;

            case "rooftop_obj":
                break;

            case "window_obj":
                break;

            case "rookie_01":
                // Paint 50
                break;

            case "tree_obj":
                break;

            case "bushes_obj":
                break;

            case "complete_2_levels":
                break;

            case "all_level1":
                break;

            case "all_level2":
                break;

            case "all_level3":
                break;

            case "all_level4":
                break;

            case "level1_10min":
                break;

            case "level2_5min":
                break;

            case "level3_5min":
                break;

            case "level4_5min":
                break;

            case "one_hour":
                break;

            case "complete_all_levels":
                break;

            case "complete_all_achievements":
                break;

            default:
                Debug.LogWarning($"Unlocked achievement {data.id} has no placeholder logic.");
                break;
        }
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