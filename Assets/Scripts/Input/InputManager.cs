using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    public event Action OnJump;

    public Vector2 MoveDirection => playerControls.Gameplay.Move.ReadValue<Vector2>();
    
    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Gameplay.Jump.performed += OnJumpPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke();
    }
}
