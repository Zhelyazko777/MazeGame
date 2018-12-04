namespace Game.Engine.Services
{
    using System;
    using Game.Common;
    using Game.Engine.Services.Contracts;
    using Game.Renderer.Contracts;

    using static Common.ConsoleClientConstants;

    public class HelperService : IHelperService
    {
        private readonly IRenderer renderer;

        public HelperService(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public void PrintMessage(string message)
        {
            this.renderer.RenderMessageOnNewLine(message);
        }

        public void PrintMainMenu()
        {
            this.renderer.RenderMessageOnNewLine(LabelForUsableCommands);

            foreach (var option in Enum.GetNames(typeof(MenuOptions)))
            {
                this.renderer.RenderMessageOnNewLine(TabWithDashInFrontOfCommand + option);
            }
        }

        public void ClearScreen()
        {
            this.renderer.ClearScreen();
        }
    }
}
