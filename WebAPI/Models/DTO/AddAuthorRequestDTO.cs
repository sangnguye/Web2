using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class AddAuthorRequestDTO
    {
        [Required(ErrorMessage = "Author name is required")]
        [MinLength(3, ErrorMessage = "Author name must be at least 3 characters long")]
        public string FullName { get; set; }
    }
}
