using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float verticalMoveAmount = 30f;
    [SerializeField] private float moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float scaleAmount = 1.1f;

    private Vector3 startPosition;
    private Vector3 startScale;
    private Coroutine moveRoutine;

    private void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;

    }

    private IEnumerator MoveCard(bool up)
    {
        Vector3 initialPosition = transform.position;
        Vector3 initialScale = transform.localScale;

        Vector3 targetPosition = up
            ? startPosition + new Vector3(0f, verticalMoveAmount, 0f)
            : startPosition;

        Vector3 targetScale = up
            ? startScale * scaleAmount
            : startScale;

        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveTime;

            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null;
        }

        transform.position = targetPosition;
        transform.localScale = targetScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartMove(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartMove(false);
    }

    private void StartMove(bool up)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveCard(up));
    }
}
