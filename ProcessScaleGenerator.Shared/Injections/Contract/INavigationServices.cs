using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Messages;

namespace ProcessScaleGenerator.Shared.Injections.Contract;
public interface INavigationServices
{
    Task GoToPageAsync(RegisteredPages page);
    Task GoToHomeAsync();
    Task GoToBackAsync();
}
