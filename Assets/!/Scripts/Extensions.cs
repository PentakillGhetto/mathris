using UnityEngine;

public static class Extensions
{
    public static float AngleTo(this Vector2 this_, Vector2 to)
    {
        Vector2 direction = to - this_;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;
        return angle;
    }

    public static Vector3Int ToVector3Int(this Vector2Int this_) => new(this_.x, this_.y, 0);
}