using Microsoft.Azure.ServiceBus;
using System;

namespace EventBus.Azure.Interfaces
{
    /// <summary>
    /// Application lifetime persistent connection singleton
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IPersistentConnection : IDisposable
    {
        /// <summary>
        /// Gets the service bus connection string builder.
        /// </summary>
        /// <value>
        /// The service bus connection string builder.
        /// </value>
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <returns>Topic client</returns>
        ITopicClient CreateModel();
    }
}
