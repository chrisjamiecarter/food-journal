using FoodJournal.Application.Enums;

namespace FoodJournal.Application.DTOs;

public sealed record FoodFrequencyReportResponse(
    IReadOnlyCollection<FoodFrequencyResponse> Foods,
    int TotalUniqueFoods,
    DateTime ReportStartDate,
    DateTime ReportEndDate,
    ReportPeriod Period);
