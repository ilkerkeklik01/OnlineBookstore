using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Dtos
{
    public class CreatedOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        
        public List<OrderItemForOrderDto> OrderItems { get; set; }
        //OrderItem ı orderItemForOrderDto ya maplemeliyim

    }
}
