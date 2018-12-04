namespace Game.Common
{
    using Game.Common.Contracts;
    using System;

    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random rnd;

        public RandomGenerator()
        {
            this.rnd = new Random();
        }

        public int GetNumber(int minValue, int maxValue)
            => this.rnd.Next(minValue, maxValue);
            
    }
}
