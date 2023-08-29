using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderItems.Dtos
{
    public class CreatedOrderItemDto : IComparable<CreatedOrderItemDto>
    {
        public int  Id { get; set; }
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


        public int CompareTo(CreatedOrderItemDto? other)
        {
            if (other == null)
                return 1;

            int comparison = Id.CompareTo(other.Id);
            if (comparison != 0)
                return comparison;

            comparison = OrderId.CompareTo(other.OrderId);
            if (comparison != 0)
                return comparison;

            comparison = IsInTheBasket.CompareTo(other.IsInTheBasket);
            if (comparison != 0)
                return comparison;

            comparison = UserId.CompareTo(other.UserId);
            if (comparison != 0)
                return comparison;

            comparison = UserName.CompareTo(other.UserName);
            if (comparison != 0)
                return comparison;

            comparison = BookId.CompareTo(other.BookId);
            if (comparison != 0)
                return comparison;

            comparison = Quantity.CompareTo(other.Quantity);
            if (comparison != 0)
                return comparison;

            comparison = BookTitleAtThatTime.CompareTo(other.BookTitleAtThatTime);
            if (comparison != 0)
                return comparison;

            comparison = BookAuthorAtThatTime.CompareTo(other.BookAuthorAtThatTime);
            if (comparison != 0)
                return comparison;

            comparison = BookCategoryIdAtThatTime.CompareTo(other.BookCategoryIdAtThatTime);
            if (comparison != 0)
                return comparison;

            comparison = string.Compare(BookDescriptionAtThatTime, other.BookDescriptionAtThatTime, StringComparison.Ordinal);
            if (comparison != 0)
                return comparison;

            comparison = BookPriceAtThatTime.CompareTo(other.BookPriceAtThatTime);
            if (comparison != 0)
                return comparison;

            comparison = BookDiscountAtThatTime.CompareTo(other.BookDiscountAtThatTime);
            if (comparison != 0)
                return comparison;

            comparison = BookPublicationDateAtThatTime.CompareTo(other.BookPublicationDateAtThatTime);
            if (comparison != 0)
                return comparison;

            comparison = string.Compare(BookCoverImagePathAtThatTime, other.BookCoverImagePathAtThatTime, StringComparison.Ordinal);
            if (comparison != 0)
                return comparison;

            return 0; // All properties are equal
        }

    }
}
