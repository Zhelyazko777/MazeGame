namespace Game.Extentions
{
    using System.Collections.Generic;

    public static class ICollectionExtentions
    {
        public static void AppendElementsFrom<T>(this ICollection<T> baseCollectionItems, IEnumerable<T> itemsToCopy)
        {
            foreach (var item in itemsToCopy)
            {
                baseCollectionItems.Add(item);
            }
        }
    }
}
