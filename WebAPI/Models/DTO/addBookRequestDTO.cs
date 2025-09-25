using System.ComponentModel.DataAnnotations;
using WebAPI.Models.Domain;
namespace WebAPI.Models.DTO
{
    public class AddBookRequestDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [RegularExpression(@"^[\p{L}0-9\s]+$", ErrorMessage = "Title cannot contain special characters")]
        [MinLength(10)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        //navigational Properties - 
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
