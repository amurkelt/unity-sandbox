using UnityEngine;

public enum AchievementType
{
    TotalObjectsFound,
    SpecificObjectId,
    CompleteLevel,
    CompleteAllObjectsInLevel,
    LevelUnderTime,
    TotalPlaytime,
    CompleteAllLevels,
    CompleteAllAchievements,
    CompleteXLevels
}

[CreateAssetMenu(menuName = "Achievements/Achievement")]
public class AchievementData : ScriptableObject
{
    public string id;
    public string title;
    [TextArea] public string description;

    public AchievementType type;

    public int intValue;          // object count, level index, etc
    public float floatValue;      // time requirement
    public string stringValue;    // objectId or levelName
}