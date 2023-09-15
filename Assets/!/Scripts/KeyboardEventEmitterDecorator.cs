using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class KeyboardEventEmitterDecorator : MonoBehaviour
{
    [SerializeField] private UnityEvent onFall;
    [SerializeField] private UnityEvent onMoveLeft;
    [SerializeField] private UnityEvent onMoveRight;
    [SerializeField] private UnityEvent onHardFall;
    [SerializeField] private UnityEvent onRotateLeft;
    [SerializeField] private UnityEvent onRotateRight;

    public void HandleFall(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onFall?.Invoke();
        }
    }

    public void HandleMoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onMoveLeft?.Invoke();
        }
    }


    public void HandleMoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onMoveRight?.Invoke();
        }
    }

    public void HandleHardFall(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onHardFall?.Invoke();
        }
    }

    public void HandleRotateLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onRotateLeft?.Invoke();
        }
    }

    public void HandleRotateRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onRotateRight?.Invoke();
        }
    }
}
