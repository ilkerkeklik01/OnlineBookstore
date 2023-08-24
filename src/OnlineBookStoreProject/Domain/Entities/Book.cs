using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book:Entity,IComparable<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public int? BookshelfId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? CoverImagePath { get; set; }
        public Bookshelf? Bookshelf { get; set; }
        public Category? Category { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public Book(int id, string title, string author, int categoryId, int bookShelfId, string? description, decimal price, DateTime publicationDate, string? coverImagePath) : base(id)
        {
            Title = title;
            Author = author;
            CategoryId = categoryId;
            BookshelfId = bookShelfId;
            Description = description;
            Price = price;
            PublicationDate = publicationDate;
            CoverImagePath = coverImagePath;
            //OrderItemId = orderItemId;
        }


        public Book()
        {
            
        }

        public int CompareTo(Book? other)
        {
            if (other == null)
            {
                return 1; // Null is considered greater than any non-null object
            }

            // Compare by Id
            int idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0)
            {
                return idComparison;
            }

            // Compare by Title
            int titleComparison = Title.CompareTo(other.Title);
            if (titleComparison != 0)
            {
                return titleComparison;
            }

            // Compare by Author
            int authorComparison = Author.CompareTo(other.Author);
            if (authorComparison != 0)
            {
                return authorComparison;
            }

            // Compare by CategoryId
            int categoryIdComparison = CategoryId.CompareTo(other.CategoryId);
            if (categoryIdComparison != 0)
            {
                return categoryIdComparison;
            }

            // Compare by BookshelfId
            int bookshelfIdComparison = Nullable.Compare(BookshelfId, other.BookshelfId);
            if (bookshelfIdComparison != 0)
            {
                return bookshelfIdComparison;
            }

            // Compare by Description
            int descriptionComparison = string.Compare(Description, other.Description, StringComparison.Ordinal);
            if (descriptionComparison != 0)
            {
                return descriptionComparison;
            }

            // Compare by Price
            int priceComparison = Price.CompareTo(other.Price);
            if (priceComparison != 0)
            {
                return priceComparison;
            }

            // Compare by Discount
            int discountComparison = Discount.CompareTo(other.Discount);
            if (discountComparison != 0)
            {
                return discountComparison;
            }

            // Compare by PublicationDate
            int publicationDateComparison = PublicationDate.CompareTo(other.PublicationDate);
            if (publicationDateComparison != 0)
            {
                return publicationDateComparison;
            }

            // Compare by CoverImagePath
            int coverImagePathComparison = string.Compare(CoverImagePath, other.CoverImagePath, StringComparison.Ordinal);
            if (coverImagePathComparison != 0)
            {
                return coverImagePathComparison;
            }

            // If all properties are equal, then the objects are equal
            return 0;
        }

    }
}
