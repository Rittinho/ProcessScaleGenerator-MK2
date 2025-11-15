using CommunityToolkit.Mvvm.Messaging.Messages;
using ProcessScaleGenerator.Shared.Constants;

namespace ProcessScaleGenerator.Shared.Messages;
public class PageRequestMessage(RegisteredPages page) : ValueChangedMessage<RegisteredPages>(page)
{
}
