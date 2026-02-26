using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public static PlayerInput PlayerInput;

    public Vector2 MoveInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public bool WasMenuOpenPressed { get; private set; }
    public bool WasMenuClosePressed { get; private set; }

    private InputAction moveAction;
    private InputAction mousePositionAction;
    private InputAction menuOpenAction;
    private InputAction menuCloseAction;

    // control schemes
    public static string GamepadControlScheme = "Gamepad";
    public static string KeyboardAndMouseControlScheme = "Keyboard&Mouse";

    public static string CurrentControlScheme { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerInput = GetComponent<PlayerInput>();

        moveAction = PlayerInput.actions["Movement"];
        mousePositionAction = PlayerInput.actions["MousePosition"];
        menuOpenAction = PlayerInput.actions["MenuOPEN"];
        menuCloseAction = PlayerInput.actions["MenuCLOSE"];
    }

    private void Update()
    {
        MoveInput = moveAction.ReadValue<Vector2>();
        MousePosition = mousePositionAction.ReadValue<Vector2>();
        //Debug.Log(MousePosition);

        WasMenuOpenPressed = menuOpenAction.WasPressedThisFrame();
        WasMenuClosePressed = menuCloseAction.WasPressedThisFrame();
    }

    public static void DeactivatePlayerControls()
    {
        PlayerInput.currentActionMap.Disable();
    }

    public static void ActivatePlayerControls()
    {
        PlayerInput.currentActionMap.Enable();
    }

    public void SwitchControls(PlayerInput input)
    {
        CurrentControlScheme = input.currentControlScheme;
        //Debug.Log(CurrentControlScheme);
    }
}
