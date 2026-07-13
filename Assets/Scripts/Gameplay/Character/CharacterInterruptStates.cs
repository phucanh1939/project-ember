using System;

namespace Game.Gameplay
{
    public class CharacterInterruptStates<TStateId> where TStateId : Enum
    {
        public TStateId Dead { get; set; }
        public TStateId Stunned { get; set; }
        public TStateId Knockback { get; set; }
    }
}
