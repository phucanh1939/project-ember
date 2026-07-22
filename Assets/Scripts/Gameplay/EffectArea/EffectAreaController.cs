using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Bridges Unity trigger events and lifecycle updates to a runtime EffectArea.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class EffectAreaController : MonoBehaviour
    {
        [SerializeField] private EffectAreaDefinition _definition;

        private EffectArea _effectArea;

        private void Awake()
        {
            var instigator = GetComponent<IEffectInstigator>();
            _effectArea = _definition.CreateEffectArea(instigator);
        }

        private void Update()
        {
            _effectArea.Update(Time.deltaTime);

            if (_effectArea.IsExpired)
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var target = other.GetComponentInParent<IEffectTarget>();

            if (target == null)
                return;

            _effectArea.Enter(target);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var target = other.GetComponentInParent<IEffectTarget>();

            if (target == null)
                return;

            _effectArea.Exit(target);
        }

        private void OnDestroy()
        {
            _effectArea?.Expire();
        }
    }
}