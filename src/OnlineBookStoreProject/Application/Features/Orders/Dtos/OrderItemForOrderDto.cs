using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Dtos
{
    public class OrderItemForOrderDto
    {

        public int Id { get; set; }
        public int OrderId { get; set; } = 0;
        public bool IsInTheBasket { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        //public decimal Discount { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        
        //public string BookTitle { get; set; }
        //public decimal BookPrice { get; set; }

        public string BookTitleAtThatTime { get; set; }
        public string BookAuthorAtThatTime { get; set; }
        public int BookCategoryIdAtThatTime { get; set; }
        public string? BookDescriptionAtThatTime { get; set; }
        public decimal BookPriceAtThatTime { get; set; }
        public decimal BookDiscountAtThatTime { get; set; }
        public DateTime BookPublicationDateAtThatTime { get; set; }
        public string? BookCoverImagePathAtThatTime { get; set; }

    }
}
