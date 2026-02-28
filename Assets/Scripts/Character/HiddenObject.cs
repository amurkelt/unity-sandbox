using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HiddenObject : MonoBehaviour
{
    [SerializeField] private string objectId;
    [SerializeField] private RandomSoundPlayer randomSoundPlayer;
    [SerializeField] private SpriteRenderer sprite;
    private bool found;

    private void Reset()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (string.IsNullOrEmpty(objectId))
            objectId = gameObject.name;

        randomSoundPlayer = FindFirstObjectByType<RandomSoundPlayer>();
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
            ApplyFoundVisual();
    }

    private void OnMouseDown()
    {
        if (found) return;

        found = true;

        PlayerPrefs.SetInt(objectId, 1);

        randomSoundPlayer.PlayAudio();
        ApplyFoundVisual();
    }

    private void ApplyFoundVisual()
    {
        sprite.color = Color.grey;
    }
}