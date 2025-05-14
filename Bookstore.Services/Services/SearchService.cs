using Bookstore.Server.Data.Models;
using Bookstore.Server.Repositories;

namespace Bookstore.Server.Services;

public class SearchService
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Magazine> _magazineRepository;

    public SearchService(IRepository<Book> bookRepository, IRepository<Magazine> magazineRepository)
    {
        _bookRepository = bookRepository;
        _magazineRepository = magazineRepository;
    }

    public List<object> SearchByPrefix(string prefix)
    {
        var results = new List<object>();
        prefix = prefix.ToLower();
        
        var bookMatches = PrefixSearch(_bookRepository.GetSortedByName(), prefix);
        var magazineMatches = PrefixSearch(_magazineRepository.GetSortedByName(), prefix);
        
        results.AddRange(bookMatches);
        results.AddRange(magazineMatches);
        
        return results;
    }

    private List<T> PrefixSearch<T>(SortedList<string, List<T>> sortedList, string prefix)
    {
        var keys = sortedList.Keys;
        var start = BinarySearchPrefix(keys, prefix);
        var results = new List<T>();

        for (int i = start; i < keys.Count && keys[i].StartsWith(prefix); i++)
        {
            results.AddRange(sortedList[keys[i]]);
        }

        return results;
    }

    private int BinarySearchPrefix(IList<string> keys, string prefix)
    {
        int low = 0, high = keys.Count - 1;

        while (low <= high)
        {
            int mid = (low+high)/2;
            if (string.Compare(keys[mid], prefix, StringComparison.OrdinalIgnoreCase) < 0)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        return low;
    }
}