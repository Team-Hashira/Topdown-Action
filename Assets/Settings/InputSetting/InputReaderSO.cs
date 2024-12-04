using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
{
    private Controls _controls;

    #region Actions

    public event Action OnAttackEvent;
    public event Action OnInteractEvent;
    public event Action<bool> OnSprintEvent;

    #endregion


    #region Values

    public Vector2 Movement { get; private set; }
    public Vector2 MouseScreenPos { get; private set; }

    #endregion

    private void OnEnable()
    {
        if (_controls == null)
            _controls = new Controls();

        _controls.Player.AddCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAttackEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInteractEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnSprintEvent?.Invoke(true);
        else if (context.canceled)
            OnSprintEvent?.Invoke(false);
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        MouseScreenPos = context.ReadValue<Vector2>();
    }
}
