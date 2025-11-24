namespace ProcessScaleGenerator.Shared.Injections.Contract;

public interface IMessagingServices
{
    void BeginInvokeOnMainThread(Action action);
    Task InvokeOnMainThreadAsync(Action action);
}
