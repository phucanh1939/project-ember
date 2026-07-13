using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Updates the character's Animator based on gameplay state.
    ///
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Movement _movement;
        [SerializeField] private Attack _attack;

        private static readonly int MoveXHash = Animator.StringToHash("MoveX");
        private static readonly int MoveYHash = Animator.StringToHash("MoveY");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 velocity = _movement.Velocity;
            Vector2 facing = _movement.FacingDirection;

            _animator.SetFloat(MoveXHash, facing.x);
            _animator.SetFloat(MoveYHash, facing.y);
            _animator.SetFloat(SpeedHash, velocity.sqrMagnitude);

            _animator.SetBool(IsAttackingHash, _attack.IsAttacking);
        }

        // ARCH:
        // CharacterAnimation observes Movement.
        // It does not receive animation commands from PlayerController.
        //
        // This keeps gameplay independent from presentation.

        // PERF:
        // Every character currently updates its Animator every frame.
        // Later we'll investigate whether parameter updates can be
        // skipped when values haven't changed.
    }
}