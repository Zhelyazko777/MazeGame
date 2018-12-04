namespace Game.Items.Contracts
{
    public interface IItem
    {
        string Name { get; }

        int Weight { get; }

        bool IsUsed { get; set; }
    }
}
