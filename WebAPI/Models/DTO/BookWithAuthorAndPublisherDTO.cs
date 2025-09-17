namespace WebAPI.Models.DTO
{
    public class BookWithAuthorAndPublisherDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }

        // Publisher name
        public string PublisherName { get; set; }

        // List of Author names
        public List<string> AuthorNames { get; set; } = new List<string>();
    }
}
