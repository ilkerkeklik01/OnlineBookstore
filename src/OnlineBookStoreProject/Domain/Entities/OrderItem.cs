using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem:Entity
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public Book? Book { get; set; }
        public Order? Order { get; set; }
        public OrderItem()
        {
            
        }

        public OrderItem(int id, int orderId, int bookId, int quantity, decimal discount) : base(id)
        {
            OrderId = orderId;
            BookId = bookId;
            Quantity = quantity;
            Discount = discount;
        }


    }
}
