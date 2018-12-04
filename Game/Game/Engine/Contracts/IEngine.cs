namespace Game.Engine.Contracts
{
    public interface IEngine
    {
        void Initialize();

        bool ProcessCommand();
    }
}
