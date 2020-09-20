using System;
using Microsoft.Azure.ServiceBus;

namespace EventBus.Azure.Interfaces
{
    public interface IPersistentConnection : IDisposable
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        ITopicClient CreateModel();
    }
}
