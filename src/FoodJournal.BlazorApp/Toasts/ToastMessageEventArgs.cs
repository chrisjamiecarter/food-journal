namespace FoodJournal.BlazorApp.Toasts;

internal sealed class ToastMessageEventArgs(ToastMessage message) : EventArgs
{
    public ToastMessage Message { get; } = message;
}
