namespace Game.Reader
{
    using System;
    using Contracts;

    public class ConsoleReader : IReader
    {
        public string Read() => Console.ReadLine();

        public int ReadInt() => int.Parse(Console.ReadLine());
    }
}
