using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<BookServiceModel>> GetBook(int id)
        {
            var book =  await this.books.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet]
        public async Task<IEnumerable<BookServiceModel>> GetBooks()
        {
            return  await this.books.GetBooks();
        }

        [HttpPost]
        public async Task<ActionResult<BookServiceModel>> PostBook(BookServiceModel book)
        {
            var bookId = await this.books.RegisterBook(book);

            return CreatedAtAction(nameof(GetBook), new { id = bookId }, book); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookServiceModel book)
        {
            var isUpdated = await this.books.UpdateBook(id, book);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookServiceModel>> Delete(int id)
        {
            var bookToDelete = await this.books.DeleteBook(id);

            if (bookToDelete == null)
            {
                return NotFound();
            }

            return bookToDelete;
        }
    }
}
