namespace Game.Engine.Services
{
    using System.Collections.Generic;
    using Common;
    using Contracts;
    using Common.Contracts;
    using Rooms.Contracts;
    using Reader.Contracts;
    using Renderer.Contracts;
    using Players.Contracts;

    using static Common.ConsoleClientConstants;

    public class InputProviderService : IInputProviderService
    {
        private readonly IRenderer renderer;
        private readonly IReader reader;
        private readonly IGameFactory gameFactory;

        public InputProviderService(IRenderer renderer, IReader reader, IGameFactory gameFactory)
        {
            this.renderer= renderer;
            this.reader = reader;
            this.gameFactory = gameFactory;
        }

        public IEnumerable<IPlayer> InitializePlayers(IRoom startRoom)
        {
            this.renderer.RenderWithoutNewLine(TypeNumberOfPlayersLabel);
            int numOfPlayers = this.reader.ReadInt();
            var listOfPlayers = new List<IPlayer>();

            for (int i = 0; i < numOfPlayers; i++)
            {
                this.renderer.RenderMessageOnNewLine(InvitationLabelForPlayerName);
                var name = this.reader.Read();
                this.renderer.RenderMessageOnNewLine(WelcomeMessage + name + ExclamationMarkChar);
                listOfPlayers.Add(gameFactory.CreatePlayer(name, this.gameFactory.CreateBackpakc(), GlobalConstants.PlayerMaxHealthPoints, startRoom));
            }

            return listOfPlayers;
        }

        public string ReadCommand(string playerName)
        {
            this.renderer.RenderWithoutNewLine(playerName + Colon);

            return this.reader.Read();
        }
    }
}
