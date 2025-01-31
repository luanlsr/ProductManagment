using Bogus;
using FluentAssertions;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.ValueObjects;

namespace ProductManagment.Test.UnitTest.Domain
{
    public class OrderTest
    {
        private readonly Faker _faker;
        private readonly Guid _clientId;
        private readonly List<OrderItem> _items;

        public OrderTest()
        {
            _faker = new Faker("pt_BR");
            _clientId = Guid.NewGuid();
            _items = new List<OrderItem>
            {
                new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 2, 50.0m),
                new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 1, 100.0m)
            };
        }

        [Fact]
        public void ShouldCreateOrder()
        {
            var order = new Order(_clientId, _items);

            order.ClientId.Should().Be(_clientId);
            order.Items.Should().HaveCount(2);
            order.Total.Should().Be(200.0m);
            order.Status.Should().Be(OrderStatus.Created);
        }

        [Fact]
        public void ShouldUpdateOrderStatus()
        {
            var order = new Order(_clientId, _items);
            order.UpdateStatus(OrderStatus.Shipped);
            order.Status.Should().Be(OrderStatus.Shipped);
        }
    }
}
