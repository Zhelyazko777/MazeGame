namespace Game.Engine.Services
{
    using System;
    using System.Linq;
    using Common;
    using Contracts;
    using Items.Contracts;
    using Rooms.Contracts;
    using Renderer.Contracts;

    using static Common.ConsoleClientConstants;

    public class LocationService : ILocationService
    {
        private readonly IRenderer renderer;

        public LocationService(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public IRoom ChangeLocation(string destination, IRoom currentRoom, IItem key)
        {
            Validator.CheckStringIfNullOrEmpty(destination);

            var destinationExit = currentRoom.Exits.Where(e => e.SecondRoom.Name.ToLower() == destination.ToLower()).FirstOrDefault();

            if (destinationExit != null && ((!destinationExit.IsLocked) || (key != null && key.IsUsed == false)))
            {
                if (destinationExit.IsLocked)
                {
                    key.IsUsed = true;
                }
            }
            else if (destinationExit != null && destinationExit.IsLocked)
            {
                throw new InvalidOperationException(LockedExitMsg);
            }
            else
            {
                throw new InvalidOperationException(RoomEnterErrorMessage);
            }

            return destinationExit.SecondRoom;
        }

        public void PrintCurrentLocation(IRoom currentRoom)
        {
            Validator.CheckStringIfNullOrEmpty(currentRoom.Name);

            this.renderer.RenderMessageOnNewLine(CurrentRoomNameLabel + currentRoom.Name);
            this.renderer.RenderMessageOnNewLine(ExitsLabel);

            foreach (var exit in currentRoom.Exits)
            {
                var isLockedText = exit.IsLocked ? LockedMsg : UnlockedMsg;
                this.renderer.RenderMessageOnNewLine(TabWithDashInFrontOfCommand + exit.SecondRoom.Name + TabWithDashInFrontOfCommand + isLockedText);
            }
        }
    }
}
