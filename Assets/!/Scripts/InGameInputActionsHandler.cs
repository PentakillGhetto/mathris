using UnityEngine;
using UnityEngine.InputSystem;

public class InGameInputActionsHandler : MonoBehaviour
{
    public void HandleFall(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Fall");
        }
    }

    public void HandleMoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Move left");
        }
    }

    public void HandleMoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Move right");
        }
    }

    public void HandleNextSign(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Next sign");
        }
    }
}
