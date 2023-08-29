using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeEventEmitter : MonoBehaviour
{
    private Vector2 currentPosition;

    public void HandleTouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            currentPosition = context.ReadValue<Vector2>();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log(Vector2.Angle(currentPosition, context.ReadValue<Vector2>()));
        }
    }
}
