using Bogus;
using FluentAssertions;
using ProductManagment.Domain.Entities;
using System;
using Xunit;

namespace ProductManagment.Test.UnitTest.Domain
{
    public class ClientTest
    {
        private readonly Faker _faker;

        public ClientTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public void ShouldCreateClient()
        {
            // Arrange
            var userId = _faker.Random.Guid().ToString();
            var name = _faker.Person.FullName;
            var email = _faker.Internet.Email();
            var phone = _faker.Phone.PhoneNumber();
            var document = _faker.Random.ReplaceNumbers("###########");

            // Act
            var client = new Client(userId, name, email, phone, document);

            // Assert
            client.Should().NotBeNull();
            client.UserId.Should().Be(userId);
            client.Name.Should().Be(name);
            client.Email.Should().Be(email);
            client.Phone.Should().Be(phone);
            client.Document.Should().Be(document);
        }

        [Fact]
        public void ShouldCreateClientFromAnotherClient()
        {
            // Arrange
            var originalClient = new Client(
                _faker.Random.Guid().ToString(),
                _faker.Person.FullName,
                _faker.Internet.Email(),
                _faker.Phone.PhoneNumber(),
                _faker.Random.ReplaceNumbers("###########")
            );

            // Act
            var copiedClient = new Client(originalClient.UserId,originalClient.Name, originalClient.Email, originalClient.Phone, originalClient.Document);

            // Assert
            copiedClient.Should().NotBeNull();
            copiedClient.UserId.Should().Be(originalClient.UserId);
            copiedClient.Name.Should().Be(originalClient.Name);
            copiedClient.Email.Should().Be(originalClient.Email);
            copiedClient.Phone.Should().Be(originalClient.Phone);
            copiedClient.Document.Should().Be(originalClient.Document);
        }

        [Fact]
        public void ShouldUpdateClient()
        {
            // Arrange
            var client = new Client(
                _faker.Random.Guid().ToString(),
                _faker.Person.FullName,
                _faker.Internet.Email(),
                _faker.Phone.PhoneNumber(),
                _faker.Random.ReplaceNumbers("###########")
            );

            var newName = _faker.Person.FullName;
            var newEmail = _faker.Internet.Email();
            var newPhone = _faker.Phone.PhoneNumber();

            // Act
            client.Update(newName, newEmail, newPhone);

            // Assert
            client.Name.Should().Be(newName);
            client.Email.Should().Be(newEmail);
            client.Phone.Should().Be(newPhone);
        }

        [Theory]
        [InlineData("", "Valid Name", "email@example.com", "999999999", "12345678900")]
        [InlineData("123", "", "email@example.com", "999999999", "12345678900")]
        [InlineData("123", "Valid Name", "", "999999999", "12345678900")]
        [InlineData("123", "Valid Name", "email@example.com", "999999999", "")]
        public void ShouldThrowExceptionWhenCreatingClientWithInvalidData(string userId, string name, string email, string phone, string document)
        {
            // Act
            Action act = () => new Client(userId, name, email, phone, document);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
