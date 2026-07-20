using UnityEngine;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Coordinates the enemy's gameplay components.
    /// The controller stores shared components required by states.
    /// </summary>
    [RequireComponent(typeof(Sensor))]
    public class EnemyController : CharacterController
    {
        [SerializeField] private Sensor _sensor;

        public Sensor Sensor => _sensor;

        protected override void OnValidate()
        {
            base.OnValidate();
            _sensor = GetComponent<Sensor>();
        }
    }
}