using ProcessScaleGenerator.Shared.Injections.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Sevices.Implementation;

public class MessagingServices : IMessagingServices
{
    public void BeginInvokeOnMainThread(Action action)
    {
        MainThread.BeginInvokeOnMainThread(action);
    }

    public Task InvokeOnMainThreadAsync(Action action)
    {
        return MainThread.InvokeOnMainThreadAsync(action);
    }
}
