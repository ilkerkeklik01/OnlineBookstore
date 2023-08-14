using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order:Entity
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public User? User { get; set;}
        
        
        public Order()
        {
            
        }


        public Order(int id, int userId, DateTime orderDate, decimal totalPrice) : base(id)
        {
            UserId = userId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
        }
    }
}
