using FoodJournal.Application.DTOs;
using FoodJournal.Application.Repositories;

namespace FoodJournal.Application.Services;

public interface IReportService
{
    Task<List<AvailableFoodResponse>> GetAvailableFoodsAsync(
        string userId,
        CancellationToken cancellationToken);

    Task<FoodFrequencyReportResponse> GetFoodFrequencyReportAsync(
        string userId,
        FoodFrequencyRequest request,
        CancellationToken cancellationToken);

    Task<SummaryReportResponse> GetSummaryReportAsync(
        string userId,
        CancellationToken cancellationToken);
}

internal sealed class ReportService(IReportRepository repository) : IReportService
{
    private readonly IReportRepository _repository = repository;

    public Task<List<AvailableFoodResponse>> GetAvailableFoodsAsync(string userId, CancellationToken cancellationToken)
    {
        return _repository.GetAvailableFoodsAsync(userId, cancellationToken);
    }

    public Task<FoodFrequencyReportResponse> GetFoodFrequencyReportAsync(
        string userId,
        FoodFrequencyRequest request,
        CancellationToken cancellationToken)
    {
        return _repository.GetFoodFrequencyReportAsync(userId, request, cancellationToken);
    }

    public async Task<SummaryReportResponse> GetSummaryReportAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        return await _repository.GetSummaryReportAsync(userId, cancellationToken);
    }
}
