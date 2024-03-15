using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    public event Action<bool> OnParry;
    public event Action OnAttack;

    public Vector2 MoveDirection => 
        playerControls.Gameplay.Move.ReadValue<Vector2>();
    
    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Gameplay.Parry.performed += OnParryPerformedAndCanceled;
        playerControls.Gameplay.Parry.canceled += OnParryPerformedAndCanceled;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
    }    

    private void OnParryPerformedAndCanceled(InputAction.CallbackContext context)
    {
        OnParry?.Invoke(context.ReadValueAsButton());
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
}
