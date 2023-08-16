using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderItems.Dtos
{
    public class CreatedOrderItemDto
    {
        public int  Id { get; set; }
        public int OrderId { get; set; } = 0;
        public bool IsInTheBasket { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal Discount { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string BookTitle { get; set; }
        public decimal BookPrice { get; set; }

    }
}
