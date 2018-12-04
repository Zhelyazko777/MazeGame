namespace Game
{
    using Game.Backpacks;
    using Game.Backpacks.Contracts;
    using Game.Common;
    using Game.Common.Contracts;
    using Game.Engine;
    using Game.Engine.Contracts;
    using Game.Engine.Services;
    using Game.Engine.Services.Contracts;
    using Game.Exits;
    using Game.Exits.Contracts;
    using Game.Items;
    using Game.Items.Contracts;
    using Game.Players;
    using Game.Players.Contracts;
    using Game.Reader;
    using Game.Reader.Contracts;
    using Game.Renderer;
    using Game.Renderer.Contracts;
    using Game.Rooms;
    using Game.Rooms.Contracts;
    using Ninject.Modules;
    using System.Reflection;

    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRandomGenerator>().To<RandomGenerator>().InSingletonScope();
            this.Bind<IEngine>().To<StandartEngine>().InSingletonScope();
            this.Bind<IGameFactory>().To<GameFactory>().InSingletonScope();
            this.Bind<IExit>().To<Exit>();
            this.Bind<IBackpack>().To<Backpack>();
            this.Bind<IPlayer>().To<Player>();
            this.Bind<IRoom>().To<Room>();
            this.Bind<IRenderer>().To<ConsoleRenderer>();
            this.Bind<IReader>().To<ConsoleReader>();
            this.Bind<IItem>().To<Item>();
            this.Bind<IItemService>().To<ItemService>();
            this.Bind<IHealthService>().To<HealthService>();
            this.Bind<ILocationService>().To<LocationService>();
            this.Bind<IHelperService>().To<HelperService>();
            this.Bind<IInputProviderService>().To<InputProviderService>();
            this.Bind<IGameEndService>().To<GameEndService>();
            this.Bind<IInitializationService>().To<InitializationService>();
        }
    }
}
