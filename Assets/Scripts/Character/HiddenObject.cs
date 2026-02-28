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
        found = PlayerPrefs.GetInt(objectId, 0) == 1;
        if (found)
            ChangeColor();
    }

    private void OnMouseDown()
    {
        if (found) return;

        found = true;
        GameManager.Instance.AddScore(1);
        PlayerPrefs.SetInt(objectId, 1);

        randomSoundPlayer.PlayAudio();
        ChangeColor();
    }

    private void ChangeColor()
    {
        sprite.color = Color.grey;
    }

    public void ResetFound()
    {
        found = false;
        PlayerPrefs.SetInt(objectId, 0);
        sprite.color = Color.white;
    }
}