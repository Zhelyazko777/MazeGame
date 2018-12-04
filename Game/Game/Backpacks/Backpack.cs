namespace Game.Backpacks
{
    using System.Linq;
    using System.Collections.Generic;
    using Common;
    using Contracts;
    using Items.Contracts;
    using Extentions;

    public class Backpack : IBackpack
    {
        private readonly ICollection<IItem> items;

        public Backpack()
        {
            this.items = new List<IItem>();
        }

        public int AvailableWeight => GlobalConstants.BackpackMaxWeight - this.Items.Sum(i => i.Weight);

        public ICollection<IItem> Items
        {
            get
            {
                return new List<IItem>(this.items);
            }
        }

        public void AddItem(IItem item)
        {
            this.items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            this.items.Remove(item);
        }

        public void RemoveUsedItems()
        {
            var usedItems = this.items.Where(i => i.IsUsed).ToList();

            for (int i = 0; i < usedItems.Count; i++)
            {
                this.RemoveItem(usedItems[i]);
            }
        }
    }
}
