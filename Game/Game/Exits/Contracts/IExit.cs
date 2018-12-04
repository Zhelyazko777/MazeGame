namespace Game.Exits.Contracts
{
    using Game.Rooms.Contracts;

    public interface IExit
    {
        bool IsLocked { get; }

        IRoom FirstRoom { get; }

        IRoom SecondRoom { get; }
    }
}
