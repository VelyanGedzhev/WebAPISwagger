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

        public int RegisterBook(BookServiceModel book)
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

            var bookId = GetRegisteredBookId(book.Name, book.AuthorName);
            return bookId;
        }
        

        public BookServiceModel GetBook(int id)
            => this.data
                .Books
                .Where(b => b.Id == id)
                .Select(b => new BookServiceModel
                {
                    Name = b.Name,
                    AuthorName = b.AuthorName,
                    Price = b.Price,
                    Year = b.Year
                })
               .FirstOrDefault();

        public IEnumerable<BookServiceModel> GetBooks() 
            => this.data
                .Books
                .Select(b => new BookServiceModel
                {
                    Name = b.Name,
                    AuthorName = b.AuthorName,
                    Price = b.Price,
                    Year = b.Year
                })
                .ToList();

        public bool UpdateBook(int id, BookServiceModel book)
        {
            var bookToUpdate = IsBookExisting(id);

            if (bookToUpdate == null)
            {
                return false;
            }

            bookToUpdate.Name = book.Name;
            bookToUpdate.AuthorName = book.AuthorName;
            bookToUpdate.Year = book.Year;
            bookToUpdate.Price = book.Price;

            this.data.SaveChanges();

            return true;
        }

        public BookServiceModel DeleteBook(int id)
        {
            var bookToDelete = IsBookExisting(id);

            this.data.Remove(bookToDelete);
            this.data.SaveChanges();

            var bookToReturn = new BookServiceModel
            {
                Name = bookToDelete.Name,
                AuthorName = bookToDelete.AuthorName,
                Year = bookToDelete.Year,
                Price = bookToDelete.Price
            };

            return bookToReturn;
        }

        private int GetRegisteredBookId(string bookName, string authorName) 
            => this.data
                .Books
                .Where(b =>
                    b.Name == bookName &&
                    b.AuthorName == authorName)
                .Select(b => b.Id)
                .FirstOrDefault();

        private Book IsBookExisting(int id)
            => this.data
                .Books
                .FirstOrDefault(b => b.Id == id);
    }
}
