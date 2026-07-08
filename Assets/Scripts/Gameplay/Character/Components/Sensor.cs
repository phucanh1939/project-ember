using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Generic world sensing component.
    ///
    /// Responsibilities:
    /// - Query objects inside a defined detection area.
    /// - Return detected objects to the caller.
    ///
    /// This component does NOT:
    /// - Decide what the detected object means.
    /// - Know about Player, Enemy, NPC, or any gameplay rule.
    /// - Trigger state changes.
    ///
    /// Consumers decide:
    /// - When to scan.
    /// - What objects are relevant.
    /// - How to react to detected objects.
    ///
    /// Example:
    /// EnemyState -> scans for Player -> decides whether to chase.
    /// NPCState -> scans for Player -> decides whether to interact.
    ///
    /// Current implementation uses a circle detection area.
    /// Future extensions may support different detection shapes:
    /// - Circle
    /// - Rectangle
    /// - Cone / Field of View
    /// - Custom shapes
    /// </summary>
    public class Sensor : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField]
        private float _range = 5f;


        /// <summary>
        /// Finds the first object inside the sensor range
        /// matching the provided layer mask.
        ///
        /// The filter is provided by the caller because different
        /// systems may need to detect different types of objects.
        /// </summary>
        public Collider2D Scan(LayerMask mask)
        {
            // PERF:
            // Physics2D.OverlapCircle is acceptable for early development.
            //
            // Potential cost:
            // - Many objects scanning frequently can create physics query overhead.
            //
            // Future optimization:
            // - Use Physics2D.OverlapCircleNonAlloc()
            // - Scan at intervals instead of every frame
            // - Centralize sensor updates for large numbers of entities

            return Physics2D.OverlapCircle(
                transform.position,
                _range,
                mask
            );
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(
                transform.position,
                _range
            );
        }
#endif
    }
}