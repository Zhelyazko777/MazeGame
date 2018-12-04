namespace Game.Engine.Services.Contracts
{
    using System.Collections.Generic;
    using Game.Items.Contracts;
    using Game.Players.Contracts;

    public interface IItemService
    {
        ICollection<IItem> InitializeRoomItems();

        bool CheckIsItemAvailable(string itemName, IEnumerable<IItem> items);

        bool PickItem(string name, IPlayer player);
        
        bool DropItem(string name, IPlayer player);

        void PrintItems(IEnumerable<string> backPackItems, IEnumerable<string> roomItems, int availableWeight);

        IItem GetKey(ICollection<IItem> items);

        //void RemoveUsedItems(ICollection<IItem> items);
    }
}
