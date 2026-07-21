using UnityEngine;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// Coordinates player components and initializes player behavior.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerInstaller))]
    public class PlayerController : Character
    {
        [SerializeField] private PlayerInput _playerInput;

        public PlayerInput Input => _playerInput;

        protected override void OnValidate()
        {
            base.OnValidate();
            _playerInput = GetComponent<PlayerInput>();
        }
    }
}
