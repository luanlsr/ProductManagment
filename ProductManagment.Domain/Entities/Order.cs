using ProductManagment.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductManagment.Domain.ValueObjects;

namespace ProductManagment.Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        [Required]
        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }

        [Required]
        public DateTime OrderDate { get; private set; } = DateTime.Now;

        [Required]
        public OrderStatus Status { get; private set; } = OrderStatus.Created;

        public List<OrderItem> Items { get; private set; } = new();

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; private set; }

        public Order(Guid clientId, List<OrderItem> items)
        {
            if (clientId == Guid.Empty)
                throw new ArgumentException("ClientId is required.");

            if (items == null || items.Count == 0)
                throw new ArgumentException("Order must have at least one item.");

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

        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }
    }
}
