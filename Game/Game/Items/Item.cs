namespace Game.Items
{
    using Game.Items.Contracts;

    public class Item : IItem
    {
        public Item(string name, int weight)
        {
            this.Name = name;
            this.Weight = weight;
            this.IsUsed = false;
        }

        public string Name { get; private set; }

        public int Weight { get; private set; }

        public bool IsUsed { get; set; }
    }
}
