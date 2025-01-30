using ProductManagment.Domain.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ProductManagment.Domain.Entities
{
    public class Product : EntityBase<Guid>
    {
        [Required, StringLength(150)]
        public string Name { get; private set; }

        [StringLength(500)]
        public string Description { get; private set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; private set; }

        [Required, StringLength(100)]
        public string Category { get; private set; }

        [Required, StringLength(50)]
        public string SKU { get; private set; }

        [Required]
        public Guid StockId { get; private set; }
        public virtual Stock Stock { get; private set; }

        public Product(string name, string description, decimal price, string category, string sku, Guid stockId)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Product name is required.");

            if (string.IsNullOrEmpty(category))
                throw new ArgumentException("Category is required.");

            if (string.IsNullOrEmpty(sku))
                throw new ArgumentException("SKU is required.");

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            Name = name;
            Description = description;
            Price = price;
            Category = category;
            SKU = sku;
            StockId = stockId;
        }

        public void Update(string name, string description, decimal price, string category, string sku)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Product name is required.");

            if (string.IsNullOrEmpty(category))
                throw new ArgumentException("Category is required.");

            if (string.IsNullOrEmpty(sku))
                throw new ArgumentException("SKU is required.");

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            Name = name;
            Description = description;
            Price = price;
            Category = category;
            SKU = sku;
        }
    }
}
