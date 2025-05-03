namespace Bookstore.Server.Services;

public class SortingService
{
    public async Task<List<T>> QuickSortAsync<T>(IEnumerable<T> items, Func<T, object> keySelector, string order)
    {
        List<T> itemList = items.ToList();
        
        if (itemList.Count <= 1)
            return itemList;
        
        T pivot = itemList[0];
        List<T> lesser = new List<T>();
        List<T> greater = new List<T>();

        for (int i = 1; i < itemList.Count; i++)
        {
            int comparison = Compare(keySelector(itemList[i]), keySelector(pivot));

            if ((order == "asc" && comparison < 0) || (order == "desc" && comparison > 0))
            {
                lesser.Add(itemList[i]);
            }
            else
            {
                greater.Add(itemList[i]);
            }
        }

        List<T> sortedItems = new List<T>();
        
        var lesserSortedTask = QuickSortAsync(lesser, keySelector, order);
        var greaterSortedTask = QuickSortAsync(greater, keySelector, order);
        
        await Task.WhenAll(lesserSortedTask, greaterSortedTask);
        
        sortedItems.AddRange(lesserSortedTask.Result);
        sortedItems.Add(pivot);
        sortedItems.AddRange(greaterSortedTask.Result);
        
        return sortedItems;
    }

    private int Compare(object first, object second)
    {
        if (first is IComparable comparableFirst && second is IComparable comparableSecond)
        {
            return comparableFirst.CompareTo(comparableSecond);
        }
        throw new ArgumentException("The objects must implement IComparable");
    }
    
}