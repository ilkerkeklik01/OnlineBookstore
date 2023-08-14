namespace Application.Features.Books.Dtos
{
    public class BookDto
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


    }
}

