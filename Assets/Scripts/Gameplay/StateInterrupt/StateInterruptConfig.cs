namespace Game.Gameplay
{
    [System.Serializable]
    public struct StateInterruptConfig
    {
        public StateInterruptType type;
        public CharacterStateId stateId;
        public int priority;
    }
}