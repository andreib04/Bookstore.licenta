using Bookstore.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers;

[Controller]
[Route("/api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly SearchService _searchService;
    
    public SearchController(SearchService searchService) => _searchService = searchService;

    [HttpGet("{query}")]
    public IActionResult Search(string query)
    {
        try
        {
    
            var results = _searchService.SearchByPrefix(query);
            return Ok(results);
            
        }
        catch (Exception ex)
        {
            throw new Exception($"Search failed: {ex.Message}");
        }
        
    }
}