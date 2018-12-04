namespace Game.Common.Contracts
{
    using Game.Backpacks.Contracts;
    using Game.Exits.Contracts;
    using Game.Items.Contracts;
    using Game.Players.Contracts;
    using Game.Rooms;
    using Game.Rooms.Contracts;

    public interface IGameFactory
    {
        IRoom CreateRoom(string name, bool isThereMonster);
        IExit CreateExit(IRoom firstRoom, IRoom secondRoom, bool isLocked);
        IPlayer CreatePlayer(string name, IBackpack backpack, int health, IRoom startRoom);
        IItem CreateItem(string name, int weight);
        IBackpack CreateBackpakc();
    }
}
