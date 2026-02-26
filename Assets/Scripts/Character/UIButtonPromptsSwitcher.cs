using UnityEngine;

public class UIButtonPromptsSwitcher : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SwitchUIButtonPrompts()
    {
        if (InputManager.CurrentControlScheme == InputManager.KeyboardAndMouseControlScheme)
        {
            Debug.Log("Switched to KeyboardAndMouse");
        }
        else if (InputManager.CurrentControlScheme == InputManager.GamepadControlScheme)
        {
            Debug.Log("Switched to Gamepad");
        }
    }
}
