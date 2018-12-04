namespace Game.Rooms.Contracts
{
    using System.Collections.Generic;
    using Game.Exits.Contracts;
    using Game.Items.Contracts;

    public interface IRoom
    {
        string Name { get; }

        bool IsThereMonster { get; }

        ICollection<IItem> Items { get; } 

        ICollection<IExit> Exits { get; }

        void AddExit(IExit exit);

        void RemoveExit(IExit exit);

        void AddItem(IItem item);

        void RemoveItem(IItem item);

        void AddItemsCollection(ICollection<IItem> items);
    }
}
