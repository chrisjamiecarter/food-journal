using FoodJournal.BlazorApp.Toasts;

namespace FoodJournal.BlazorApp.Services;

public interface IToastService
{
    event EventHandler<ToastMessageEventArgs>? OnShow;
    void Show(string message, ToastType toastType = ToastType.Info);
    void ShowError(string message);
    void ShowInfo(string message);
    void ShowSuccess(string message);
    void ShowWarning(string message);
}

internal class ToastService : IToastService
{
    public event EventHandler<ToastMessageEventArgs>? OnShow;

    public void Show(string message, ToastType toastType = ToastType.Info)
    {
        OnShow?.Invoke(this, new ToastMessageEventArgs(new ToastMessage(message, toastType)));
    }

    public void ShowError(string message) => Show(message, ToastType.Error);
    
    public void ShowInfo(string message) => Show(message, ToastType.Info);

    public void ShowSuccess(string message) => Show(message, ToastType.Success);

    public void ShowWarning(string message) => Show(message, ToastType.Warning);
}
