using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, IMovementInput
{
    private PlayerInputActions inputActions;
    private Vector2 keyboardInput;
    private VirtualJoystick virtualJoystick;

    // Biến để xác định thiết bị nào vừa mới tác động
    private bool isJoystickActive = false;

    void Awake()
    {
        inputActions = new PlayerInputActions();

        virtualJoystick = Object.FindFirstObjectByType<VirtualJoystick>();
        //if (virtualJoystick != null)
        //{
        //    Debug.Log("Tìm thấy joystick");
        //}
        //else
        //{
        //    Debug.Log("Không thấy joystick");
        //}
    }

    void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += OnKeyboardMove;
        inputActions.Player.Move.canceled += OnKeyboardMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnKeyboardMove;
        inputActions.Player.Move.canceled -= OnKeyboardMove;

        inputActions.Player.Disable();
    }

    private void OnKeyboardMove(InputAction.CallbackContext context)
    {
        keyboardInput = context.ReadValue<Vector2>();

        if (keyboardInput != Vector2.zero)
        {
            isJoystickActive = false;
        }
    }

    public Vector2 GetMoveInput()
    {
        Vector2 jInput = (virtualJoystick != null) ? virtualJoystick.Input : Vector2.zero;

        if (jInput != Vector2.zero)
        {
            isJoystickActive = true;
        }

        if (isJoystickActive && jInput != Vector2.zero)
        {
            return jInput;
        }

        return keyboardInput;
    }
}
