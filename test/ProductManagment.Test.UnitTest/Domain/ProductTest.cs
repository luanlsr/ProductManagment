using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Bogus;
using FluentAssertions;
using ProductManagment.Domain.Entities;
using Xunit.Abstractions;

namespace ProductManagment.Test.UnitTest.Domain
{
    public class ProductTest : IDisposable
    {
        private readonly Fixture _fixture;
        private Faker _faker;
        private readonly ITestOutputHelper _outputHelper;
        private readonly Product _produtoEsperado;

        public ProductTest(ITestOutputHelper outputHelper)
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());
            _faker = new Faker("pt_BR");

            _outputHelper = outputHelper;

            _produtoEsperado = new Product(
                _faker.Commerce.ProductName(),
                _faker.Commerce.ProductDescription(),
                _faker.Random.Decimal(1, 1000),
                _faker.Commerce.Categories(1)[0],
                _faker.Random.AlphaNumeric(10),
                Guid.NewGuid()
            );
        }

        public void Dispose()
        {
            _outputHelper.WriteLine("Limpando construtor");
        }

        // Testa a criação de um produto válido
        [Fact]
        public void ShouldCreateProduct()
        {
            // Act
            var product = new Product(
                _produtoEsperado.Name,
                _produtoEsperado.Description,
                _produtoEsperado.Price,
                _produtoEsperado.Category,
                _produtoEsperado.SKU,
                _produtoEsperado.StockId);

            // Assert
            product.Name.Should().Be(_produtoEsperado.Name);
            product.Description.Should().Be(_produtoEsperado.Description);
            product.Price.Should().Be(_produtoEsperado.Price);
            product.Category.Should().Be(_produtoEsperado.Category);
            product.SKU.Should().Be(_produtoEsperado.SKU);
            product.StockId.Should().Be(_produtoEsperado.StockId);
        }

        // ✅ Testa a criação do produto com valores inválidos
        [Theory]
        [InlineData("", "Descrição válida", 10.99, "Categoria válida", "SKU123")]
        [InlineData("Produto", "", 10.99, "Categoria válida", "SKU123")]
        [InlineData("Produto", "Descrição válida", 0, "Categoria válida", "SKU123")]
        [InlineData("Produto", "Descrição válida", -5, "Categoria válida", "SKU123")]
        [InlineData("Produto", "Descrição válida", 10.99, "", "SKU123")]
        [InlineData("Produto", "Descrição válida", 10.99, "Categoria válida", "")]
        public void ShouldThrowExceptionWhenCreatingProductWithInvalidData(string name, string description, decimal price, string category, string sku)
        {
            // Act
            Action act = () => new Product(name, description, price, category, sku, Guid.NewGuid());

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        // ✅ Testa a atualização do produto
        [Fact]
        public void ShouldUpdateProduct()
        {
            // Arrange
            var product = _produtoEsperado;
            var newName = _faker.Commerce.ProductName();
            var newDescription = _faker.Commerce.ProductDescription();
            var newPrice = _faker.Random.Decimal(1, 1000);
            var newCategory = _faker.Commerce.Categories(1)[0];
            var newSKU = _faker.Random.AlphaNumeric(10);

            // Act
            product.Update(newName, newDescription, newPrice, newCategory, newSKU);

            // Assert
            product.Name.Should().Be(newName);
            product.Description.Should().Be(newDescription);
            product.Price.Should().Be(newPrice);
            product.Category.Should().Be(newCategory);
            product.SKU.Should().Be(newSKU);
        }

        // ✅ Testa a atualização do produto com valores inválidos
        [Theory]
        [InlineData("", "Descrição válida", 10.99, "Categoria válida", "SKU123")]
        [InlineData("Produto", "", 10.99, "Categoria válida", "SKU123")]
        [InlineData("Produto", "Descrição válida", 0, "Categoria válida", "SKU123")]
        [InlineData("Produto", "Descrição válida", -5, "Categoria válida", "SKU123")]
        [InlineData("Produto", "Descrição válida", 10.99, "", "SKU123")]
        [InlineData("Produto", "Descrição válida", 10.99, "Categoria válida", "")]
        public void ShouldThrowExceptionWhenUpdatingProductWithInvalidData(string name, string description, decimal price, string category, string sku)
        {
            // Arrange
            var product = _produtoEsperado;

            // Act
            Action act = () => product.Update(name, description, price, category, sku);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}

