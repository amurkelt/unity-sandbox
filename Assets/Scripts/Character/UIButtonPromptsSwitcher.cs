using UnityEngine;

public class UIButtonPromptsSwitcher : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Invoke(nameof(SwitchUIButtonPrompts), 0.1f);
    }

    public void SwitchUIButtonPrompts()
    {
        if (CursorControllerComplex.Instance == null) return;

        if (InputManager.CurrentControlScheme == InputManager.KeyboardAndMouseControlScheme)
        {
            CursorControllerComplex.Instance.SetToMode(ModeOfCursor.Pointer);
            Debug.Log("Switched to KeyboardAndMouse");
        }
        else if (InputManager.CurrentControlScheme == InputManager.GamepadControlScheme)
        {
            CursorControllerComplex.Instance.SetToMode(ModeOfCursor.Default);
            Debug.Log("Switched to Gamepad");
        }
    }
}
