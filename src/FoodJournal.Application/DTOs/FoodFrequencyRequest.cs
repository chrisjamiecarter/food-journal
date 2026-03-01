using FoodJournal.Application.Enums;

namespace FoodJournal.Application.DTOs;

public sealed record FoodFrequencyRequest(
    Guid? FoodId,
    string? FoodName,
    DateTime? StartDate,
    DateTime? EndDate,
    ReportPeriod Period = ReportPeriod.Month);
