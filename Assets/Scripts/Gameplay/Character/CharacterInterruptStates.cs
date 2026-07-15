using System;

namespace Game.Gameplay
{
    /// <summary>
    /// Maps common character interruptions to state identifiers.
    /// </summary>
    public class CharacterInterruptStates<TStateId> where TStateId : Enum
    {
        public TStateId Dead { get; set; }
        public TStateId Stunned { get; set; }
        public TStateId Knockback { get; set; }
    }
}
