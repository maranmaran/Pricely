using System;

namespace EventBus.Infrastructure.Models
{
    /// <summary>
    /// Event base
    /// </summary>
    public class Event
    {
        public Event()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
