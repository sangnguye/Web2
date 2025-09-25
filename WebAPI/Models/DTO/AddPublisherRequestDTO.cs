using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class AddPublisherRequestDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Publisher name is required")]
        [MinLength(2, ErrorMessage = "Publisher name must be at least 2 characters long")]
        public string Name { get; set; }
    }
}
