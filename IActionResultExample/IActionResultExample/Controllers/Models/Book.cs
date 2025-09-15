namespace IActionResultExample.Controllers.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int? BookId { get; set; }

        public override string ToString()
        {
            return $"book id: {Id}";
        }
    }
}
