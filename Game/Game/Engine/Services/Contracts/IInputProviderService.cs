namespace Game.Engine.Services.Contracts
{
    using System.Collections.Generic;
    using Rooms.Contracts;
    using Players.Contracts;

    public interface IInputProviderService
    {
        string ReadCommand(string playerName);

        IEnumerable<IPlayer> InitializePlayers(IRoom startRoom);
    }
}
