using Game.Players.Contracts;

namespace Game.Engine.Services.Contracts
{
    public interface  IGameEndService
    {
        void PrintDeathPlayerMessage();

        void PrintPlayerWinMessage();

        bool CheckIsPlayerAlive(IPlayer player);

        bool CheckIsPlayerWin(IPlayer player);
    }
}
