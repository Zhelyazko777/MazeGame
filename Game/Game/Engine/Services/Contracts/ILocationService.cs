namespace Game.Engine.Services.Contracts
{
    using Items.Contracts;
    using Rooms.Contracts;

    public interface ILocationService
    {
        IRoom ChangeLocation(string destination, IRoom currentRoom, IItem key);

        void PrintCurrentLocation(IRoom currentRoom);
    }
}
