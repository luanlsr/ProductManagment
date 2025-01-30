using ProductManagment.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagment.Domain.Entities
{
    public class OrderItem : EntityBase<Guid>
    {
        [Required]
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }

        [Required]
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        [Required]
        public int Quantity { get; private set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; private set; }

        public OrderItem(Guid orderId, Guid productId, int quantity, decimal unitPrice)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException("OrderId is required.");

            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId is required.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.");

            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal TotalPrice()
        {
            return Quantity * UnitPrice;
        }
    }
}
