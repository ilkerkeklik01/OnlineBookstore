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
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? CoverImagePath { get; set; }

        public Book(int id,string title, string author, int categoryId, string? description, decimal price, DateTime publicationDate, string? coverImagePath)
        {   
            this.Id = id;
            Title = title;
            Author = author;
            CategoryId = categoryId;
            Description = description;
            Price = price;
            PublicationDate = publicationDate;
            CoverImagePath = coverImagePath;
        }
        public Book()
        {
            
        }
    }
}
