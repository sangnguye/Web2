using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositories;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok(_authorRepository.GetAllAuthors());
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody]AddAuthorRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var added = _authorRepository.AddAuthor(dto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = added }, added);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorNoIdDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = _authorRepository.UpdateAuthorById(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            var deleted = _authorRepository.DeleteAuthorById(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
