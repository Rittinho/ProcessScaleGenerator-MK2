using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.Injections.Contract;

public interface IMessagingServices
{
    void BeginInvokeOnMainThread(Action action);
    Task InvokeOnMainThreadAsync(Action action);
}
