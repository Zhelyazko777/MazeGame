namespace Game.Engine
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Common;
    using Contracts;
    using Extentions;
    using Rooms.Contracts;
    using Players.Contracts;
    using Services.Contracts;

    using static Common.ConsoleClientConstants;

    public class StandartEngine : IEngine
    {
        private readonly IInitializationService initializationService;
        private readonly IItemService itemService;
        private readonly IHealthService healthService;
        private readonly IGameEndService gameEndService;
        private readonly IHelperService helperService;
        private readonly IInputProviderService inputProviderService;
        private readonly ILocationService locationService;
        
        private ICollection<IPlayer> players;

        public StandartEngine(IItemService itemService, IHelperService helperService,
                ILocationService locationService, IInputProviderService inputProviderService,
                IGameEndService gameEndService, IHealthService healthService, IInitializationService initializationService)
        {
            this.initializationService = initializationService;
            this.itemService = itemService;
            this.locationService = locationService;
            this.inputProviderService = inputProviderService;
            this.helperService = helperService;
            this.gameEndService = gameEndService;
            this.healthService = healthService;
            this.players = new List<IPlayer>();
        }
        
        public void Initialize()
        {
            var availableRooms = this.initializationService.InitializeRooms();
            var currentRoom = availableRooms.FirstOrDefault();
            
            var initializedItems = this.itemService.InitializeRoomItems();
            currentRoom.AddItemsCollection(initializedItems);

            var players = this.inputProviderService.InitializePlayers(currentRoom);
            this.players.AppendElementsFrom(players);
            this.helperService.PrintMainMenu();
        }

        public bool ProcessCommand()
        {
            for (int j = 0; j < this.players.Count; j++)
            {
                var player = this.players.ElementAt(j);

                try
                {
                    var command = this.inputProviderService.ReadCommand(player.Name);

                    if (command == MenuOptions.help.ToString())
                    {
                        this.helperService.PrintMainMenu();
                        j--;
                    }
                    else if (command == MenuOptions.items.ToString())
                    {
                        this.itemService.PrintItems(player.Backpack.Items.Select(i => i.Name),
                                                    player.CurrentRoom.Items.Select(i => i.Name),
                                                    player.Backpack.AvailableWeight);
                        j--;
                    }
                    else if (command == MenuOptions.location.ToString())
                    {
                        this.locationService.PrintCurrentLocation(player.CurrentRoom);
                        j--;
                    }
                    else if (command == MenuOptions.health.ToString())
                    {
                        this.healthService.PrintHealthStatus(player.Health);
                        j--;
                    }
                    else if (command == MenuOptions.clear_screen.ToString())
                    {
                        this.helperService.ClearScreen();
                        j--;
                    }
                    else if (command.Contains(MenuOptions.pick.ToString()))
                    {
                        var itemName = command.Split(WhiteSpace).Last();

                        if (this.itemService.PickItem(itemName, player))
                        {
                            this.helperService.PrintMessage(SuccessPickItemMessage);
                        }
                    }
                    else if (command.Contains(MenuOptions.drop.ToString()))
                    {
                        var itemName = command.Split(WhiteSpace).Last();

                        if (this.itemService.DropItem(itemName, player))
                        {
                            this.helperService.PrintMessage(SuccessPickItemMessage);
                        }
                    }
                    else if (command.Contains(MenuOptions.go.ToString()))
                    {
                        var roomName = command.Split(WhiteSpace).Last();

                        var key = this.itemService.GetKey(player.Backpack.Items);
                        var newRoom = this.locationService.ChangeLocation(roomName, player.CurrentRoom, key);
                        if (newRoom != null)
                        {
                            newRoom.AddItemsCollection(this.itemService.InitializeRoomItems());
                            this.healthService.DecreasePlayersHealth(player, newRoom.IsThereMonster);
                            player.CurrentRoom = newRoom;
                            this.helperService.PrintMessage(RoomSuccessfulEnterMessage);
                        }
                    }
                    else if (command == MenuOptions.pass_turn.ToString())
                    {
                        continue;
                    }
                    else if (command == MenuOptions.quite.ToString())
                    {
                        return false;
                    }
                    else
                    {
                        throw new InvalidOperationException(WrongCommandNameMsg);
                    }

                    if (this.gameEndService.CheckIsPlayerWin(player))
                    {
                        this.gameEndService.PrintPlayerWinMessage();
                        return false;
                    }
                    else if (!this.gameEndService.CheckIsPlayerAlive(player))
                    {
                        this.gameEndService.PrintDeathPlayerMessage();
                        return false;
                    }

                    this.healthService.IncreasePlayersHealthIfHealthpack(player);
                    player.Backpack.RemoveUsedItems();
                }
                catch (InvalidOperationException ex)
                {
                    j--;
                    this.helperService.PrintMessage(ex.Message);
                }
            }

            return true;
        }
    }
}
