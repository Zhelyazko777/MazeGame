namespace Game.Engine.Services
{
    using System.Linq;
    using Contracts;
    using Game.Common;
    using Game.Renderer.Contracts;
    using Players.Contracts;

    using static Common.ConsoleClientConstants;

    public class GameEndService : IGameEndService
    {
        private readonly IRenderer renderer;

        public GameEndService(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public bool CheckIsPlayerAlive(IPlayer player)
        {
            if (player.Health < 1)
            {
                return false;
            }

            return true;
        }

        public bool CheckIsPlayerWin(IPlayer player)
        {
            if (player.Backpack.Items.Select(i => i.Name).Contains(RoomItems.AutoWinItem.ToString()))
            {
                return true;
            }

            return false;
        }

        public void PrintDeathPlayerMessage()
        {
            this.renderer.RenderMessageOnNewLine(GameOverMessage);
        }

        public void PrintPlayerWinMessage()
        {
            this.renderer.RenderMessageOnNewLine(PlayerWinMessage);
        }
    }
}
