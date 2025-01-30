using ProductManagment.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace ProductManagment.Domain.Entities
{
    public class Stock : EntityBase<Guid>
    {
        [Required]
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        [Required]
        public int Quantity { get; private set; }

        public Stock(Guid productId, int quantity)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId is required.");

            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            ProductId = productId;
            Quantity = quantity;
        }
        public void IncreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Increase amount must be greater than zero.");

            Quantity += amount;
        }

        public void DecreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Decrease amount must be greater than zero.");

            if (Quantity - amount < 0)
                throw new InvalidOperationException("Not enough stock available.");

            Quantity -= amount;
        }
    }
}