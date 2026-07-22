using System.Collections.Generic;

namespace Game.Gameplay
{
    public interface IStatModifierReceiver
    {
        int AddModifier(StatModifier modifier);
        int AddModifiers(IReadOnlyList<StatModifier> modifiers);
        void RemoveModifiers(int id);
    }
}