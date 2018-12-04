namespace Game.Common
{
    using Game.Backpacks.Contracts;
    using Game.Common.Contracts;
    using Game.Exits.Contracts;
    using Game.Items.Contracts;
    using Game.Players.Contracts;
    using Game.Rooms.Contracts;
    using Ninject;
    using Ninject.Parameters;

    public class GameFactory : IGameFactory
    {
        private readonly IKernel kernel;

        public GameFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IBackpack CreateBackpakc()
            => this.kernel.Get<IBackpack>();

        public IExit CreateExit(IRoom firstRoom, IRoom secondRoom, bool isLocked)
            => this.kernel.Get<IExit>(new ConstructorArgument("firstRoom", firstRoom), new ConstructorArgument("secondRoom", secondRoom), new ConstructorArgument("isLocked", isLocked));

        public IItem CreateItem(string name, int weight) => this.kernel.Get<IItem>(new ConstructorArgument("name", name), new ConstructorArgument("weight", weight));

        public IPlayer CreatePlayer(string name, IBackpack backpack, int health, IRoom currentRoom)
            => this.kernel.Get<IPlayer>(new ConstructorArgument("name", name), new ConstructorArgument("backpack", backpack),
                        new ConstructorArgument("health", health), new ConstructorArgument("currentRoom", currentRoom));

        public IRoom CreateRoom(string name, bool isThereMonster) 
            => this.kernel.Get<IRoom>(new ConstructorArgument("name", name), new ConstructorArgument("isThereMonster", isThereMonster));
    }
}
