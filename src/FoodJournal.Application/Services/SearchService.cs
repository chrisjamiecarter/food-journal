using FoodJournal.Application.DTOs;
using FoodJournal.Application.Repositories;

namespace FoodJournal.Application.Services;

public interface ISearchService
{
    Task<List<AvailableFoodResponse>> GetAvailableFoodsAsync(
        CancellationToken cancellationToken);

    Task<SearchMealsResponse> SearchMealsAsync(
        string userId,
        SearchMealsRequest request,
        CancellationToken cancellationToken);
}

public class SearchService(
    ISearchRepository searchRepository) : ISearchService
{
    private readonly ISearchRepository _searchRepository = searchRepository;

    public async Task<List<AvailableFoodResponse>> GetAvailableFoodsAsync(CancellationToken cancellationToken)
    {
        return await _searchRepository.GetAvailableFoodsAsync(cancellationToken);
    }

    public async Task<SearchMealsResponse> SearchMealsAsync(string userId, SearchMealsRequest request, CancellationToken cancellationToken)
    {
        return await _searchRepository.SearchMealsAsync(userId, request, cancellationToken);
    }
}
