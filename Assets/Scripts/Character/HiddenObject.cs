using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class HiddenObject : MonoBehaviour
{
    [SerializeField] private string objectId;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private RandomSoundPlayer randomSoundPlayer;
    private bool found;

    private void Reset()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (string.IsNullOrEmpty(objectId))
            objectId = gameObject.name;

        randomSoundPlayer = FindFirstObjectByType<RandomSoundPlayer>();

        var collider = GetComponent<PolygonCollider2D>();
        if (collider != null)
        {
            collider.useDelaunayMesh = true;
        }
    }

    //private void Awake()
    //{
    //    sprite = GetComponent<SpriteRenderer>();

    //    if (string.IsNullOrEmpty(objectId))
    //        objectId = gameObject.name;
    //}

    private void Start()
    {
        var levelData = SaveManager.GetLevel(GameManager.Instance.LevelName);
        found = levelData.foundObjectIds.Contains(objectId);

        if (found)
            ChangeColor();
    }

    private void OnMouseDown()
    {
        if (found) return;
        if (PauseManager.Instance.IsPaused) return;

        found = true;

        var levelData = SaveManager.GetLevel(GameManager.Instance.LevelName);
        if (!levelData.foundObjectIds.Contains(objectId))
            levelData.foundObjectIds.Add(objectId);

        SaveManager.SaveLevel(levelData);

        randomSoundPlayer.PlayAudio();
        Animate();
        ChangeColor();

        GameManager.Instance.AddScore(1);
    }

    private void Animate()
    {

    }

    private void ChangeColor()
    {
        sprite.color = Color.grey;
    }

    public void ResetFound()
    {
        found = false;
        sprite.color = Color.white;
    }
}