using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPISwagger.Data.Models;
using WebAPISwagger.Services;

namespace WebAPISwagger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService books;

        public BooksController(IBookService books)
        {
            this.books = books;
        }

        [HttpGet]
        public IEnumerable<BookServiceModel> Get(int id)
        {
            return this.books.GetBooks(id);
        }

        [HttpPost]
        public void Post(BookServiceModel book)
        {
            this.books.CreateBook(book);
        }

        [HttpPut]
        public void Put(int id, BookServiceModel book)
        {
            this.books.UpdateBook(id, book);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var isDeleted = this.books.DeleteBook(id);

            if (!isDeleted)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
