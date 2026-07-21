using UnityEngine;

namespace Game.Core
{
    public static class Vector2Utils
    {
        public static float DirectionToAngle(Vector2 direction)
        {
            if (direction.sqrMagnitude <= Mathf.Epsilon)
                return 0f;

            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        public static Quaternion AngleToRotation(float angle)
        {
            return Quaternion.Euler(0f, 0f, angle);
        }

        public static Vector2 RotateByAngle(Vector2 vector, float angle)
        {
            return AngleToRotation(angle) * vector;
        }

        public static Vector2 RotateByDirection(Vector2 vector, Vector2 direction)
        {
            var angle = DirectionToAngle(direction);
            return RotateByAngle(vector, angle);
        }
    }
}