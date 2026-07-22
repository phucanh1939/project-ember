namespace Game.Gameplay
{
    public interface IStateInterruptor
    {
        public int AddInterrupt(StateInterruptType type);
        public void RemoveInterrupt(int id);
    }
}