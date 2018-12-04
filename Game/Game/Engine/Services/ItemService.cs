namespace Game.Engine.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Game.Common;
    using Game.Common.Contracts;
    using Game.Engine.Services.Contracts;
    using Game.Items.Contracts;
    using Game.Players.Contracts;
    using Game.Renderer.Contracts;

    using static Common.ConsoleClientConstants;

    public class ItemService : IItemService
    {
        private readonly IRandomGenerator random;
        private readonly IRenderer renderer;
        private readonly IGameFactory gameFactory;
        
        public ItemService(IRandomGenerator random, IGameFactory gameFactory, IRenderer renderer)
        {
            this.random = random;
            this.gameFactory = gameFactory;
            this.renderer = renderer;
        }

        public ICollection<IItem> InitializeRoomItems()
        {
            var numbersOfItems = random.GetNumber(GlobalConstants.MinNumberOfItemsInRoom, GlobalConstants.MaxNumberOfItemsInRoom);
            var listOfItems = new List<IItem>();

            for (int i = 0; i < numbersOfItems; i++)
            {
                var item = this.random.GetNumber(0, Enum.GetNames(typeof(RoomItems)).Length);

                if (item == (int)RoomItems.Key)
                {
                    listOfItems.Add(this.gameFactory.CreateItem(RoomItems.Key.ToString(), GlobalConstants.KeyWeight));
                }
                else if (item == (int)RoomItems.Bomb)
                {
                    listOfItems.Add(this.gameFactory.CreateItem(RoomItems.Bomb.ToString(), GlobalConstants.BombWeight));
                }
                else if (item == (int)RoomItems.HealthPack)
                {
                    listOfItems.Add(this.gameFactory.CreateItem(RoomItems.HealthPack.ToString(), GlobalConstants.HealthPackWeight));
                }
                else if (item == (int)RoomItems.Sword)
                {
                    listOfItems.Add(this.gameFactory.CreateItem(RoomItems.Sword.ToString(), GlobalConstants.SwordWeight));
                }
                else if (item == (int)RoomItems.AutoWinItem)
                {
                    listOfItems.Add(this.gameFactory.CreateItem(RoomItems.AutoWinItem.ToString(), GlobalConstants.AutoWinItemWeigth));
                }
            }

            return listOfItems;
        }

        public bool CheckIsItemAvailable(string itemName, IEnumerable<IItem> items)
            => items.Where(i => i.IsUsed == false).Select(i => i.Name.ToLower()).Contains(itemName.ToLower());

        public bool PickItem(string name, IPlayer player)
        {
            Validator.CheckStringIfNullOrEmpty(name);

            var roomItems = player.CurrentRoom.Items;

            if (this.CheckIsItemAvailable(name, roomItems))
            {
                var itemFromRoom = roomItems.Where(i => i.Name.ToLower() == name.ToLower()).First();

                if (itemFromRoom.Weight <= player.Backpack.AvailableWeight)
                {
                    player.CurrentRoom.RemoveItem(itemFromRoom);
                    player.Backpack.AddItem(itemFromRoom);
                    
                    return true;
                }
                else
                {
                    throw new InvalidOperationException(TooManyItemsInBagMessage);
                }
            }
            else
            {
                throw new InvalidOperationException(WrongItemNameMessage);
            }
        }

        public bool DropItem(string name, IPlayer player)
        {
            Validator.CheckStringIfNullOrEmpty(name);

            if (this.CheckIsItemAvailable(name, player.Backpack.Items))
            {
                var itemFromBag = player.Backpack.Items.Where(i => i.Name.ToLower() == name.ToLower()).First();
                player.Backpack.RemoveItem(itemFromBag);
                player.CurrentRoom.AddItem(itemFromBag);
                
                return true;
            }
            else
            {
                throw new InvalidOperationException(WrongItemNameMessage);
            }
        }

        public void PrintItems(IEnumerable<string> backPackItems, IEnumerable<string> roomItems, int availableWeight)
        {
            this.RenderItems(backPackItems, false);
            this.renderer.RenderMessageOnNewLine(LabelForAvailableWeight + availableWeight);
            this.RenderItems(roomItems, true);
        }

        public IItem GetKey(ICollection<IItem> items)
            => items.Where(i => i.Name == RoomItems.Key.ToString()).FirstOrDefault();

        
        private void RenderItems(IEnumerable<string> items, bool isRoom)
        {
            if (isRoom)
            {
                if (items.Count() < 1)
                {
                    this.renderer.RenderMessageOnNewLine(NoRoomItemsMessage);
                }
                else
                {
                    this.renderer.RenderMessageOnNewLine(LabelForRoomAvailableItems + String.Join(ItemInArrayDelemiter, items));
                }
            }
            else
            {
                if (items.Count() < 1)
                {
                    this.renderer.RenderMessageOnNewLine(EmptyBagMessage);
                }
                else
                {
                    this.renderer.RenderMessageOnNewLine(LabelForBagAvailableItems + String.Join(ItemInArrayDelemiter, items));
                }
            }
        }
    }
}
