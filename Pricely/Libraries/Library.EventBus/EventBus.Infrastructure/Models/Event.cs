using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Infrastructure.Models
{
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
