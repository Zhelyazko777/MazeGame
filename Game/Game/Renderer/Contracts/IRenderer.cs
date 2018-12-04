namespace Game.Renderer.Contracts
{
    public interface IRenderer
    {
        //void RenderMainMenu();

        //void RenderRoomItems(IEnumerable<string> items);

        //void RenderBackPackItems(IEnumerable<string> items);

        //void RenderAvailableWeight(int availableWeight);

        //void RenderLabelForNameInput();

        //void RenderWelcomeMessage(string name);

        //void RenderPlayerHealthStatus(int healthPoints);

        //void RenderCommandLabel();

        //void RenderCurrentRoom(string name);

        //void RenderAvailableRooms(IEnumerable<string> rooms);

        //void RenderSuccessfulPickItemMessage();

        //void RenderErrorItemMessage();

        //void RenderSuccessfulChangeRoomMessage();

        //void RenderErrorChangeRoomMessage();

        //void RenderSuccesfulDropItem();

        //void RenderTooManyItemsInBagMessage();

        //void RenderWrongCommand();

        //void RenderDeadHeroMessage();

        //void RenderVictoryMessage();

        void RenderWithoutNewLine(string message);

        void RenderMessageOnNewLine(string message);

        void ClearScreen();
    }
}
