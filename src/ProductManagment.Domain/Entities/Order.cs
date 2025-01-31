using ProductManagment.Domain.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductManagment.Domain.ValueObjects;
using System.Collections.Generic;

namespace ProductManagment.Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        [Required]
        public Guid ClientId { get; private set; }
        public virtual Client Client { get; private set; }

        [Required]
        public DateTime OrderDate { get; private set; } = DateTime.Now;

        [Required]
        public virtual OrderStatus Status { get; private set; } = OrderStatus.Created;

        public List<OrderItem> Items { get; private set; } = new();

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; private set; }

        private Order() { }

        public Order(Guid clientId, List<OrderItem> items)
        {

            Validate(clientId, items);
            
            ClientId = clientId;
            Items = items;
            Total = CalculateTotal();
        }

        private decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.TotalPrice();
            }
            return total;
        }

        public void RecalculateTotal()
        {
            Total = 0;
            foreach (var item in Items)
            {
                Total += item.TotalPrice();
            }
        }


        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }

        private void Validate(Guid clientId, List<OrderItem> items)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException("ClientId is required.");

            if (items == null || items.Count == 0)
                throw new ArgumentException("Order must have at least one item.");
        }
    }
}
