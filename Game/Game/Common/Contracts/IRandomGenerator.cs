namespace Game.Common.Contracts
{
    public interface IRandomGenerator
    {
        int GetNumber(int minValue, int maxValue);
    }
}
