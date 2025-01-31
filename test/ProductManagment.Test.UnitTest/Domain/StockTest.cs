using Bogus;
using FluentAssertions;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Test.UnitTest.Domain
{
    public class StockTest
    {
        private readonly Faker _faker;
        private readonly Guid _productId;

        public StockTest()
        {
            _faker = new Faker("pt_BR");
            _productId = Guid.NewGuid();
        }

        [Fact]
        public void ShouldCreateStock()
        {
            var stock = new Stock(_productId, 10);
            stock.ProductId.Should().Be(_productId);
            stock.Quantity.Should().Be(10);
        }

        [Fact]
        public void ShouldIncreaseStock()
        {
            var stock = new Stock(_productId, 10);
            stock.IncreaseStock(5);
            stock.Quantity.Should().Be(15);
        }

        [Fact]
        public void ShouldDecreaseStock()
        {
            var stock = new Stock(_productId, 10);
            stock.DecreaseStock(5);
            stock.Quantity.Should().Be(5);
        }

        [Fact]
        public void ShouldThrowExceptionWhenDecreasingStockBelowZero()
        {
            var stock = new Stock(_productId, 10);
            Action act = () => stock.DecreaseStock(15);
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
