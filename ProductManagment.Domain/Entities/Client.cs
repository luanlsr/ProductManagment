using ProductManagment.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace ProductManagment.Domain.Entities
{
    public class Client : EntityBase<Guid>
    {
        [Required]
        public string UserId { get; private set; }

        [Required, StringLength(100)]
        public string Name { get; private set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; private set; }

        [Phone, StringLength(20)]
        public string Phone { get; private set; }

        [Required, StringLength(14)]
        public string Document { get; private set; }

        public DateTime RegistrationDate { get; private set; } = DateTime.Now;

        public List<Order> Orders { get; private set; } = new();

        public Client(string userId, string name, string email, string phone, string document)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("UserId is required.");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Client name is required.");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrEmpty(document))
                throw new ArgumentException("Document is required.");

            UserId = userId;
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
            RegistrationDate = DateTime.Now;
        }

        public void Update(string name, string email, string phone)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Client name is required.");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email is required.");

            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}
