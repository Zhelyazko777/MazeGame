namespace Game.Backpacks.Contracts
{
    using Game.Items.Contracts;
    using System.Collections.Generic;

    public interface IBackpack
    {
        int AvailableWeight { get; }

        ICollection<IItem> Items { get; }

        void AddItem(IItem item);

        void RemoveItem(IItem item);

        void RemoveUsedItems();
    }
}
