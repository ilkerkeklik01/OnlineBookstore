using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bookshelves.Dtos
{
    public class CreatedBookshelfDto :IComparable<CreatedBookshelfDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }


        public int CompareTo(CreatedBookshelfDto? other)
        {
            if (other == null)
            {
                return 1; // This object is greater because the other object is null
            }

            // Compare properties in the desired order
            int idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0)
            {
                return idComparison;
            }

            int nameComparison = Name.CompareTo(other.Name);
            if (nameComparison != 0)
            {
                return nameComparison;
            }

            int userIdComparison = UserId.CompareTo(other.UserId);
            if (userIdComparison != 0)
            {
                return userIdComparison;
            }

            return UserName.CompareTo(other.UserName);
        }


    }
}
