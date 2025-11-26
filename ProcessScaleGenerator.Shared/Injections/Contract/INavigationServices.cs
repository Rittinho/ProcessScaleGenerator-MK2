using ProcessScaleGenerator.Shared.Constants;

namespace ProcessScaleGenerator.Shared.Injections.Contract;

public interface INavigationServices
{
    Task GoToPageAsync(RegisteredPages page);
    object GetViewAsync(RegisteredPages page);
    Task GoToHomeAsync();
    Task GoToBackAsync();
}
