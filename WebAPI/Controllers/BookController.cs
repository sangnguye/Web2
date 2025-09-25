using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPI.CustomActionFilter;
using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;
using WebAPI.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IBookRepository _bookRepository;

        public BooksController(AppDbContext dbContext, IBookRepository bookRepository)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAll()
        {
            // su dung reposity pattern
            var allBooks = _bookRepository.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = _bookRepository.GetBookById(id);
            return Ok(bookWithIdDTO);
        }
        [HttpPost("add-book")]
        [ValidateModel]
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            if (addBookRequestDTO == null)
                return BadRequest("Book data is required");

            // Kiểm tra PublisherId tồn tại trong DB
            if (!_dbContext.Publishers.Any(p => p.Id == addBookRequestDTO.PublisherId))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.PublisherId), "Publisher does not exist");
                return BadRequest(ModelState);
            }

            if (ValidateAddBook(addBookRequestDTO))
            {
                var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
                return Ok(bookAdd);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            if (bookDTO == null)
                return BadRequest("Book data is required");

            // Kiểm tra PublisherId tồn tại
            if (!_dbContext.Publishers.Any(p => p.Id == bookDTO.PublisherId))
            {
                ModelState.AddModelError(nameof(bookDTO.PublisherId), "Publisher does not exist");
                return BadRequest(ModelState);
            }

            var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
            return Ok(updateBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var deleteBook = _bookRepository.DeleteBookById(id);
            return Ok(deleteBook);
        }

        #region Private methods 
        private bool ValidateAddBook(AddBookRequestDTO addBookRequestDTO)
        {
            if (addBookRequestDTO == null)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO), $"Please add book data");
                return false;
            }
            // kiem tra Description NotNull
            if (string.IsNullOrEmpty(addBookRequestDTO.Description))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Description), $"{nameof(addBookRequestDTO.Description)} cannot be null");

            }
            // kiem tra rating (0,5) 
            if (addBookRequestDTO.Rate < 0 || addBookRequestDTO.Rate > 5)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Rate), $"{nameof(addBookRequestDTO.Rate)} cannot be less than 0 and more than 5");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
