using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Dtos;

namespace Application.Features.Users.Dtos
{
    public class CreatedUserDto : IComparable<CreatedUserDto>
    {
        
        public int Id{ get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime PasswordUpdatedAt { get; set; }


        public int CompareTo(CreatedUserDto? other)
        {
            if (other == null)
            {
                return 1; // If the other object is null, consider this object greater.
            }

            // Compare by the Id property.
            int idComparison = this.Id.CompareTo(other.Id);
            if (idComparison != 0)
            {
                return idComparison;
            }

            // Compare by the Username property.
            int usernameComparison = string.Compare(this.Username, other.Username, StringComparison.OrdinalIgnoreCase);
            if (usernameComparison != 0)
            {
                return usernameComparison;
            }

            // Compare by the Email property.
            int emailComparison = string.Compare(this.Email, other.Email, StringComparison.OrdinalIgnoreCase);
            if (emailComparison != 0)
            {
                return emailComparison;
            }

            // Compare by the Password property.
            int passwordComparison = string.Compare(this.Password, other.Password);
            if (passwordComparison != 0)
            {
                return passwordComparison;
            }

            // Compare by the RegistrationDate property.
            int registrationDateComparison = this.RegistrationDate.CompareTo(other.RegistrationDate);
            if (registrationDateComparison != 0)
            {
                return registrationDateComparison;
            }

            // Compare by the PasswordUpdatedAt property.
            int passwordUpdatedAtComparison = this.PasswordUpdatedAt.CompareTo(other.PasswordUpdatedAt);
            if (passwordUpdatedAtComparison != 0)
            {
                return passwordUpdatedAtComparison;
            }

            // If all properties are equal, consider the objects equal.
            return 0;
        }

    }
}
