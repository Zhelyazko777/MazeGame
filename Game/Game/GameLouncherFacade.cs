namespace Game
{
    using Engine.Contracts;
    using Ninject;

    public static class GameLouncherFacade
    {
        public static void StartGame()
        {
            var kernel = new StandardKernel(new IocModule());
            var engine = kernel.Get<IEngine>();

            engine.Initialize();

            while (true)
            {
                var result = engine.ProcessCommand();
                if (!result)
                {
                    return;
                }
            }
        }
    }
}
