using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User :Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? PasswordUpdatedAt { get; set; }
        public ICollection<Bookshelf> Bookshelves { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public User()
        {
            
        }

        public User(int id,string username, string email, string password, DateTime registrationDate, DateTime? passwordUpdatedAt)
        {
            this.Id = id;
            Username = username;
            Email = email;
            Password = password;
            RegistrationDate = registrationDate;
            PasswordUpdatedAt = passwordUpdatedAt;
        }
    }
}
