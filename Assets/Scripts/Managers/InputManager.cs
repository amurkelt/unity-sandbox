using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public static PlayerInput PlayerInput;

    public Vector2 MoveInput { get; private set; }
    public bool MenuOpenInput { get; private set; }
    public bool UIMenuCloseInput { get; private set; }

    private InputAction moveInputAction;
    private InputAction menuOpenAction;
    private InputAction UIMenuCloseAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerInput = GetComponent<PlayerInput>();

        moveInputAction = PlayerInput.actions["Movement"];
        menuOpenAction = PlayerInput.actions["MenuOPEN"];
        UIMenuCloseAction = PlayerInput.actions["MenuCLOSE"];
    }

    private void Update()
    {
        MoveInput = moveInputAction.ReadValue<Vector2>();
        MenuOpenInput = menuOpenAction.WasPressedThisFrame();
        UIMenuCloseInput = UIMenuCloseAction.WasPressedThisFrame();
    }
}
