using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bookshelf:Entity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Book>? Books { get; set; }

        public Bookshelf()
        {
            
        }

        public Bookshelf(int id, string name, int userId) : base(id)
        {
            Name = name;
            UserId = userId;
        }


    }
}
