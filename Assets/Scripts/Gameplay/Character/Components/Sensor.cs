using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Generic world sensing component.
    ///
    /// Responsibilities:
    /// - Detect objects inside a defined area.
    /// - Manage scanning frequency.
    /// - Store the latest detected object.
    ///
    /// This component does NOT:
    /// - Decide what the detected object means.
    /// - Know about Player, Enemy, NPC, or any gameplay rule.
    /// - Trigger gameplay behavior.
    ///
    /// Consumers decide:
    /// - Which sensing mode should be active.
    /// - How detected objects should be handled.
    ///
    /// Future extensions:
    /// - Multiple detection targets.
    /// - Different detection shapes.
    /// - Vision cone / line of sight.
    /// - Hearing sensor.
    /// </summary>
    public class Sensor : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField] private float _range = 5f;

        [SerializeField] private float _passiveScanInterval = 0.5f;
        [SerializeField] private float _activeScanInterval = 0.1f;


        public Collider2D DetectedObject { get; private set; }

        public SensorMode Mode { get; private set; }

        private LayerMask _scanMask;
        private float _nextScanTime;


        public void SetScanMask(LayerMask mask)
        {
            _scanMask = mask;
        }


        public void SetMode(SensorMode mode)
        {
            Mode = mode;

            if (Mode == SensorMode.Disabled)
            {
                DetectedObject = null;
            }
        }


        private void Update()
        {
            if (Mode == SensorMode.Disabled)
                return;

            if (Time.time < _nextScanTime)
                return;

            _nextScanTime = Time.time + GetScanInterval();

            Scan();
        }


        private float GetScanInterval()
        {
            return Mode switch
            {
                SensorMode.Passive => _passiveScanInterval,
                SensorMode.Active => _activeScanInterval,
                _ => float.MaxValue
            };
        }


        private void Scan()
        {
            // PERF:
            // Physics2D.OverlapCircle is acceptable for early development.
            //
            // Potential cost:
            // - Many active sensors scanning frequently can create physics query overhead.
            //
            // Future optimization:
            // - Use Physics2D.OverlapCircleNonAlloc()
            // - Centralize sensor updates for large numbers of entities
            // - Batch queries when many enemies exist

            DetectedObject = Physics2D.OverlapCircle(
                transform.position,
                _range,
                _scanMask);
        }


        /// <summary>
        /// Manual one-time scan.
        ///
        /// Useful for systems that do not need continuous sensing.
        /// </summary>
        public Collider2D Scan(LayerMask mask)
        {
            return Physics2D.OverlapCircle(
                transform.position,
                _range,
                mask);
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(
                transform.position,
                _range);
        }
#endif
    }
}