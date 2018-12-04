namespace Game.Engine.Services.Contracts
{
    using System.Collections.Generic;
    using Rooms.Contracts;

    public interface IInitializationService
    {
        ICollection<IRoom> InitializeRooms();
    }
}
