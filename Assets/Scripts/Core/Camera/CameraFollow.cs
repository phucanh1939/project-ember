using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// Simple top-down camera follow system.
    ///
    /// RESPONSIBILITY:
    /// - Follow target smoothly
    /// - Maintain fixed offset
    ///
    /// NOTE:
    /// Foundation implementation. Will later support camera states and effects.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0, 0, -10); // -10 is Default camera Z position in Unity 2D
        [SerializeField] private float smoothTime = 0.15f;

        private Vector3 velocity;

        private void LateUpdate()
        {
            if (target == null) return;

            Vector3 targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                smoothTime
            );

            // PERF:
            // SmoothDamp runs every frame but is fine for a single camera system.
            // If camera logic grows (multiple cameras / effects), revisit update strategy.
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}