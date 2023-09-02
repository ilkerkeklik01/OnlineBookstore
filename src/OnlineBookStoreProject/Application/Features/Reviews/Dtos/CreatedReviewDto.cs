using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reviews.Dtos
{
    public class CreatedReviewDto : IComparable<CreatedReviewDto>
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int? Rating { get; set; }//How to be on a scale of 1 to 10
        public DateTime CreatedAt { get; set; }
        public string BookTitle { get; set; }
        public string UserName { get; set; }

        public int CompareTo(CreatedReviewDto? other)
        {
            if (other == null)
            {
                return 1; 
            }

            if (!string.Equals(this.ReviewText, other.ReviewText))
            {
                return string.Compare(this.ReviewText, other.ReviewText);
            }

            if (!string.Equals(this.BookTitle, other.BookTitle))
            {
                return string.Compare(this.BookTitle, other.BookTitle);
            }

            if (!string.Equals(this.UserName, other.UserName))
            {
                return string.Compare(this.UserName, other.UserName);
            }

            
            if (this.Rating.HasValue && other.Rating.HasValue)
            {
                return this.Rating.Value.CompareTo(other.Rating.Value);
            }
            else if (this.Rating.HasValue)
            {
                return 1; 
            }
            else if (other.Rating.HasValue)
            {
                return -1;
            }

            return 0;
        }

    }
}
