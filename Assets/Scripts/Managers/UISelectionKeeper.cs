using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISelectionKeeper : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Selectable defaultSelection;
    [SerializeField] private GameObject menuRoot;

    private GameObject lastSelected;

    private void Awake()
    {
        if (eventSystem == null)
            eventSystem = FindFirstObjectByType<EventSystem>();
    }

    private void OnEnable()
    {
        SelectDefault();
    }

    private void Update()
    {
        if (!MenuIsOpen())
            return;

        var current = eventSystem.currentSelectedGameObject;

        if (current != null)
        {
            lastSelected = current;
        }
        else if (lastSelected != null)
        {
            eventSystem.SetSelectedGameObject(lastSelected);
        }
    }

    private bool MenuIsOpen()
    {
        return menuRoot != null && menuRoot.activeInHierarchy;
    }

    public void SelectDefault()
    {
        if (eventSystem == null || defaultSelection == null)
            return;

        eventSystem.SetSelectedGameObject(defaultSelection.gameObject);
        lastSelected = defaultSelection.gameObject;
    }
}