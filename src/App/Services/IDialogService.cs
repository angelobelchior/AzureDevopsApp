using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace AzureDevops.Services;

public interface IDialogService
{
    Task ShowToast(string message);
}

internal class DialogService : IDialogService
{
    public async Task ShowToast(string message)
    {
        var cancellationTokenSource = new CancellationTokenSource();

        var duration = ToastDuration.Short;
        var fontSize = 14;
        var toast = Toast.Make(message, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }
}