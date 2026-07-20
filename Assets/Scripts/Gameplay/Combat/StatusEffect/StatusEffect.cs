using System;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Publishes status-effect events that can interrupt character behavior.
    /// </summary>
    public class StatusEffect : MonoBehaviour
    {
        public event Action OnStunned;
        public event Action OnKnockback;
    }
}
