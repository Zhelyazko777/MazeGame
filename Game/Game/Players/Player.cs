namespace Game.Players
{
    using Contracts;
    using Backpacks.Contracts;
    using Game.Common;
    using Game.Rooms.Contracts;

    public class Player: IPlayer
    {
        public Player(string name, IBackpack backpack, int health, IRoom currentRoom)
        {
            this.Name = name;
            this.Backpack = backpack;
            this.Health = health;
            this.CurrentRoom = currentRoom;
        }

        public string Name { get; private set; }

        public IRoom CurrentRoom { get; set; }

        public IBackpack Backpack { get; private set; }

        public int Health { get; set; }
    }
}
