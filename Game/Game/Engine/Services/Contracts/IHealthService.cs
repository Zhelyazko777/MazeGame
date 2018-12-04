namespace Game.Engine.Services.Contracts
{
    using Players.Contracts;

    public interface IHealthService
    {
        void DecreasePlayersHealth(IPlayer player, bool isThereMonster);

        void IncreasePlayersHealthIfHealthpack(IPlayer player);

        void PrintHealthStatus(int healthPoints);
    }
}
