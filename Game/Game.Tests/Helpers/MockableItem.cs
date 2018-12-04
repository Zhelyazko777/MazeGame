namespace Game.Tests.Helpers
{
    public class MockableItem
    {
        public MockableItem(string name, bool isUsed)
        {
            this.Name = name;
            this.IsUsed = isUsed;
        }

        public string Name { get; set; }

        public bool IsUsed { get; set; }
    }
}
