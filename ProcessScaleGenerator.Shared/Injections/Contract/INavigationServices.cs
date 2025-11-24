using ProcessScaleGenerator.Shared.Constants;

namespace ProcessScaleGenerator.Shared.Injections.Contract;

public interface INavigationServices
{
    Task GoToPageAsync(RegisteredPages page);
    Task GoToHomeAsync();
    Task GoToBackAsync();
}
