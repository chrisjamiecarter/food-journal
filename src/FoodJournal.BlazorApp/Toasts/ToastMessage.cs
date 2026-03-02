namespace FoodJournal.BlazorApp.Toasts;

public sealed record ToastMessage(
    string Message,
    ToastType Type)
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.Now;
}
