using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review :Entity
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int? Rating { get; set; }//How to be on a scale of 1 to 10
        public DateTime CreatedAt { get; set; }
        public Book? Book { get; set; }
        public User? User { get; set; }

        public Review(int id, int bookId, int userId, string reviewText, int rating, DateTime createdAt) : base(id)
        {
            BookId = bookId;
            UserId = userId;
            ReviewText = reviewText;
            Rating = rating;
            CreatedAt = createdAt;
        }

        public Review()
        {
            
        }
    }
}
