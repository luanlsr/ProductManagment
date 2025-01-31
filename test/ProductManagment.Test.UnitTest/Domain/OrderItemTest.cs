using Bogus;
using FluentAssertions;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Test.UnitTest.Domain
{
    public class OrderItemTest
    {
        private readonly Faker _faker;
        private readonly Guid _orderId;
        private readonly Guid _productId;

        public OrderItemTest()
        {
            _faker = new Faker("pt_BR");
            _orderId = Guid.NewGuid();
            _productId = Guid.NewGuid();
        }

        [Fact]
        public void ShouldCreateOrderItem()
        {
            var orderItem = new OrderItem(_orderId, _productId, 3, 100.0m);
            orderItem.OrderId.Should().Be(_orderId);
            orderItem.ProductId.Should().Be(_productId);
            orderItem.Quantity.Should().Be(3);
            orderItem.UnitPrice.Should().Be(100.0m);
            orderItem.TotalPrice().Should().Be(300.0m);
        }

        [Theory]
        [InlineData(0, 100.0)]
        [InlineData(3, 0)]
        public void ShouldThrowExceptionForInvalidOrderItem(int quantity, decimal price)
        {
            Action act = () => new OrderItem(_orderId, _productId, quantity, price);
            act.Should().Throw<ArgumentException>();
        }
    }
}
