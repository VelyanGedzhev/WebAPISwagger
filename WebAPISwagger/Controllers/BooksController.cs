using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPISwagger.Services;

namespace WebAPISwagger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService books;

        public BooksController(IBookService books)
        {
            this.books = books;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<BookServiceModel> GetBook(int id)
        {
            var book =  this.books.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet]
        public IEnumerable<BookServiceModel> GetBooks()
        {
            return this.books.GetBooks();
        }

        [HttpPost]
        public ActionResult<BookServiceModel> PostBook(BookServiceModel book)
        {
            var bookId = this.books.RegisterBook(book);

            return CreatedAtAction(nameof(GetBook), new { id = bookId }, book); 
        }

        [HttpPut("{id}")]
        public IActionResult PutBook(int id, BookServiceModel book)
        {
            var isUpdated = this.books.UpdateBook(id, book);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete]
        public ActionResult<BookServiceModel> Delete(int id)
        {
            var bookToDelete = this.books.DeleteBook(id);

            if (bookToDelete == null)
            {
                return NotFound();
            }

            return bookToDelete;
        }
    }
}
