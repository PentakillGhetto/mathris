using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class SwipeEventEmitter : MonoBehaviour
{
    [SerializeField] private UnityEvent onSwipeLeft;
    [SerializeField] private UnityEvent onSwipeUp;
    [SerializeField] private UnityEvent onSwipeRight;
    [SerializeField] private UnityEvent onSwipeDown;

    private Vector2 currentPosition;

    public void HandleTouch(InputAction.CallbackContext context)
    {
        // CallbackContext's phase is not assignable to TouchPhase enum
        // That's why we need to read the whole value
        var state = context.ReadValue<UnityEngine.InputSystem.LowLevel.TouchState>();

        if (state.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            currentPosition = state.position;
        }

        if (state.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            // Get vector between touch start and touch end by extracting start from end
            var swipeDirection = (state.position - currentPosition).normalized;
            // Normalize the result
            var normalizedSwipeDirection = swipeDirection.normalized;
            // Get 0-36- angle from normalized vector
            var angle = Vector2.zero.AngleTo(normalizedSwipeDirection);
            // We then divide a circle in 4 parts, that represent spaces as follows:
            // Left: 225deg to 135deg
            // Up: 135deg to 45deg
            // Right: 45deg to 315deg
            // Down: 315deg to 225deg
            if (angle > 135 && angle < 225)
            {
                onSwipeLeft?.Invoke();
            }
            if (angle > 45 && angle < 135)
            {
                onSwipeUp?.Invoke();
            }
            if (angle < 45 && angle > 315)
            {
                onSwipeRight?.Invoke();
            }
            if (angle > 225 && angle < 315)
            {
                onSwipeDown?.Invoke();
            }
        }
    }
}
