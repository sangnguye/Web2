using System.ComponentModel.DataAnnotations;
using WebAPI.Models.Domain;

namespace WebAPI.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
    public class AuthorNoIdDTO
    {
        [Required(ErrorMessage = "Author name is required")]
        [MinLength(3, ErrorMessage = "Author name must be at least 3 characters long")]
        public string FullName { get; set; }
    }
}
