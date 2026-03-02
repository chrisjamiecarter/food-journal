namespace FoodJournal.BlazorApp.Toasts;

public class ToastMessageEventArgs(ToastMessage message) : EventArgs
{
    public ToastMessage Message { get; } = message;
}
