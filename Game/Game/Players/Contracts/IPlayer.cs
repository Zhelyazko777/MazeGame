namespace Game.Players.Contracts
{
    using Game.Backpacks.Contracts;
    using Game.Rooms.Contracts;

    public interface IPlayer
    {
        string Name { get; }

        IRoom CurrentRoom { get; set; }

        IBackpack Backpack { get; }

        int Health { get; set; }
    }
}
