using FoodJournal.Application.Enums;

namespace FoodJournal.Application.DTOs;

public sealed record FoodFrequencyRequest(
    Guid? FoodId,
    DateTime? StartDate,
    DateTime? EndDate,
    ReportPeriod Period = ReportPeriod.Month);
