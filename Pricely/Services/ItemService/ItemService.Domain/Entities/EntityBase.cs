﻿using System;
using ItemService.Domain.Interfaces;

namespace ItemService.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}