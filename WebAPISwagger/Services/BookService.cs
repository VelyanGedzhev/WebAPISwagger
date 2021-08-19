using System.Collections.Generic;
using System.Linq;
using WebAPISwagger.Data;
using WebAPISwagger.Data.Models;

namespace WebAPISwagger.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext data;

        public BookService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void CreateBook(BookServiceModel book)
        {
            var bookToAdd = new Book
            {
                Name = book.Name,
                AuthorName = book.AuthorName,
                Price = book.Price,
                Year = book.Year
            };

            this.data.Books.Add(bookToAdd);
            this.data.SaveChanges();
        }

        public IEnumerable<BookServiceModel> GetBooks(int id = 0)
        {
            var query = this.data
                .Books
                .AsQueryable();

            if (id != 0)
            {
                query = query.Where(b => b.Id == id);
            }

            var queryList = query
                .Select(b => new BookServiceModel
                {
                    Name = b.Name,
                    AuthorName = b.AuthorName,
                    Price = b.Price,
                    Year = b.Year
                })
                .ToList();

            return queryList;
        }

        public void UpdateBook(int id, BookServiceModel book)
        {
            var bookToUpdate = IsBookExisting(id);

            if (bookToUpdate == null)
            {
                return;
            }

            bookToUpdate.Name = book.Name;
            bookToUpdate.AuthorName = book.AuthorName;
            bookToUpdate.Year = book.Year;
            bookToUpdate.Price = book.Price;

            this.data.SaveChanges();
        }

        public bool DeleteBook(int id)
        {
            var bookToDelete = IsBookExisting(id);

            if (bookToDelete == null)
            {
                return false;
            }

            this.data.Remove(bookToDelete);
            this.data.SaveChanges();

            return true;
        }

        private Book IsBookExisting(int id)
            => this.data
                .Books
                .FirstOrDefault(b => b.Id == id);
    }
}
