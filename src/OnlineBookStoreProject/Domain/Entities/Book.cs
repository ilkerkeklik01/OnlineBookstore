using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book:Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public int? BookshelfId { get; set; }
        public int? OrderItemId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? CoverImagePath { get; set; }
        public Bookshelf? Bookshelf { get; set; }
        public Category? Category { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public OrderItem OrderItem { get; set; }
        public Book(int id, string title,int orderItemId, string author, int categoryId, int bookShelfId, string? description, decimal price, DateTime publicationDate, string? coverImagePath) : base(id)
        {
            Title = title;
            Author = author;
            CategoryId = categoryId;
            BookshelfId = bookShelfId;
            Description = description;
            Price = price;
            PublicationDate = publicationDate;
            CoverImagePath = coverImagePath;
            OrderItemId = orderItemId;
        }


        public Book()
        {
            
        }

    }
}
