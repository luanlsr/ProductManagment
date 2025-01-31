using System;
using System.Collections.Generic;
using ProductManagment.Domain.ValueObjects;

namespace ProductManagment.Domain.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }
}
