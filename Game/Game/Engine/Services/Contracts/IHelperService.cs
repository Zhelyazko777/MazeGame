namespace Game.Engine.Services.Contracts
{
    public interface IHelperService
    {
        void PrintMainMenu();

        void PrintMessage(string message);

        void ClearScreen();
    }
}
