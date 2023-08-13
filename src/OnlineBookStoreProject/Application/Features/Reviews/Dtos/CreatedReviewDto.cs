using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reviews.Dtos
{
    public class CreatedReviewDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int? Rating { get; set; }//How to be on a scale of 1 to 10
        public DateTime CreatedAt { get; set; }
        public string BookTitle { get; set; }
        public string UserName { get; set; }

    }
}
