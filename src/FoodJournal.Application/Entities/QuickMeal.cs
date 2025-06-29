﻿using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using FoodJournal.Application.Primitives;

namespace FoodJournal.Application.Entities;

/// <summary>
/// Represents a quick meal entity. 
/// This is a simplified version of a meal that can be quickly logged without detailed information.
/// /// </summary>
public sealed class QuickMeal : AggregateRoot
{
    [SetsRequiredMembers]
    public QuickMeal(Guid id, string userId, string name) : base(id)
    {
        UserId = userId;
        Name = name;
    }

    public required string Name { get; set; }
    public required string UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public Collection<Food> Foods { get; } = [];
}
