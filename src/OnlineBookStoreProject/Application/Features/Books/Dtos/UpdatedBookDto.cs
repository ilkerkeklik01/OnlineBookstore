using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Dtos
{
    public class UpdatedBookDto :IComparable<UpdatedBookDto>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? CoverImagePath { get; set; }
        public decimal Discount { get; set; }

        public int CompareTo(UpdatedBookDto? other)
        {
            if (other == null)
            {
                return 1; // Null is considered greater than any non-null object
            }

            int idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0)
            {
                return idComparison;
            }

            int titleComparison = Title.CompareTo(other.Title);
            if (titleComparison != 0)
            {
                return titleComparison;
            }

            int authorComparison = Author.CompareTo(other.Author);
            if (authorComparison != 0)
            {
                return authorComparison;
            }

            int categoryIdComparison = CategoryId.CompareTo(other.CategoryId);
            if (categoryIdComparison != 0)
            {
                return categoryIdComparison;
            }

            int descriptionComparison = string.Compare(Description, other.Description, StringComparison.Ordinal);
            if (descriptionComparison != 0)
            {
                return descriptionComparison;
            }

            int priceComparison = Price.CompareTo(other.Price);
            if (priceComparison != 0)
            {
                return priceComparison;
            }

            int publicationDateComparison = PublicationDate.CompareTo(other.PublicationDate);
            if (publicationDateComparison != 0)
            {
                return publicationDateComparison;
            }

            int coverImagePathComparison = string.Compare(CoverImagePath, other.CoverImagePath, StringComparison.Ordinal);
            if (coverImagePathComparison != 0)
            {
                return coverImagePathComparison;
            }

            int discountComparison = Discount.CompareTo(other.Discount);
            return discountComparison;
        }

    }
}
