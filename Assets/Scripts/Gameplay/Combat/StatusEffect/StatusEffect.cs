using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class StatusEffect : MonoBehaviour
    {
        public event Action OnStunned;
        public event Action OnKnockback;
    }
}