using Game.Common;

namespace Game.Engine.Services
{
    using System.Collections.Generic;
    using Contracts;
    using Common.Contracts;
    using Extentions;
    using Rooms.Contracts;

    using static ConsoleClientConstants;

    public class InitializationService : IInitializationService
    {
        private readonly IGameFactory gameFactory;
        private readonly ICollection<IRoom> rooms;

        public InitializationService(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
            this.rooms = new List<IRoom>();
        }

        public ICollection<IRoom> InitializeRooms()
        {
            var firstRoom = this.gameFactory.CreateRoom(FirstRoomName, false);
            var secondRoom = this.gameFactory.CreateRoom(SecondRoomName, true);
            var thirdRoom = this.gameFactory.CreateRoom(ThirdRoomName, false);

            firstRoom.AddExit(this.gameFactory.CreateExit(firstRoom, secondRoom, false));
            firstRoom.AddExit(this.gameFactory.CreateExit(firstRoom, thirdRoom, true));
            secondRoom.AddExit(this.gameFactory.CreateExit(secondRoom, firstRoom, false));
            thirdRoom.AddExit(this.gameFactory.CreateExit(thirdRoom, firstRoom, true));

            this.rooms.Add(firstRoom);
            this.rooms.Add(secondRoom);
            this.rooms.Add(thirdRoom);

            var copiedList = new List<IRoom>();
            copiedList.AppendElementsFrom(this.rooms);

            return copiedList;
        }
    }
}
