using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class BasketItem : Entity
    {

        public int UserId { get; set; }
        public User? User { get; set; }
        public int OrderItemId { get; set; }

    }
}
