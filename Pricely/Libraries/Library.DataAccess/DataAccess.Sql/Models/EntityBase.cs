﻿using DataAccess.Sql.Interfaces;
using System;

namespace DataAccess.Sql.Models
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}