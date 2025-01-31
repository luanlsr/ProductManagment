﻿using System.ComponentModel.DataAnnotations;

namespace ProductManagment.Domain.Core.Base
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; protected set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}