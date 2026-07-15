using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines an object that a character can interact with.
    /// </summary>
    public interface IInteractable
    {
        void Interact(GameObject interactor);
    }
}
