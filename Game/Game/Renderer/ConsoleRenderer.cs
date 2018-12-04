namespace Game.Renderer
{
    using System;
    using Contracts;

    using static Common.ConsoleClientConstants;

    public class ConsoleRenderer : IRenderer
    {
        public void RenderMessageOnNewLine(string message)
        {
            Console.WriteLine(message);
        }

        public void RenderWithoutNewLine(string message)
        {
            Console.Write(message);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}
