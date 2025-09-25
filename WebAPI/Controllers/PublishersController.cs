using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositories;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public IActionResult GetAllPublishers()
        {
            return Ok(_publisherRepository.GetAllPublishers());
        }

        [HttpGet("{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = _publisherRepository.GetPublisherById(id);
            if (publisher == null) return NotFound();
            return Ok(publisher);
        }

        [HttpPost]
        public IActionResult AddPublisher(AddPublisherRequestDTO dto)
        {
            if (dto == null)
                return BadRequest("Publisher data is required");

            // Kiểm tra trùng tên
            if (_publisherRepository.ExistsByName(dto.Name))
            {
                ModelState.AddModelError(nameof(dto.Name), "Publisher name already exists");
                return BadRequest(ModelState);
            }

            var added = _publisherRepository.AddPublisher(dto);
            return CreatedAtAction(nameof(GetPublisherById), new { id = added.Id }, added);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePublisherById(int id, PublisherNoIdDTO dto)
        {
            if (dto == null)
                return BadRequest("Publisher data is required");

            if (_publisherRepository.ExistsByName(dto.Name, id))
            {
                ModelState.AddModelError(nameof(dto.Name), "Publisher name already exists");
                return BadRequest(ModelState);
            }

            var updated = _publisherRepository.UpdatePublisherById(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            var deleted = _publisherRepository.DeletePublisherById(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
