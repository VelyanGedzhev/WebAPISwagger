using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<int> RegisterBook(BookServiceModel book)
        {
            var bookToAdd = new Book
            {
                Name = book.Name,
                AuthorName = book.AuthorName,
                Price = book.Price,
                Year = book.Year
            };

            await this.data.Books.AddAsync(bookToAdd);
            await this.data.SaveChangesAsync();

            var bookId = GetRegisteredBookId(book.Name, book.AuthorName);

            return await bookId;
        }
        
        public async Task<BookServiceModel> GetBook(int id)
            => await this.data
                .Books
                .Where(b => b.Id == id)
                .Select(b => new BookServiceModel
                {
                    Name = b.Name,
                    AuthorName = b.AuthorName,
                    Price = b.Price,
                    Year = b.Year
                })
               .FirstOrDefaultAsync();

        public async Task<IEnumerable<BookServiceModel>> GetBooks() 
            => await this.data
                 .Books
                 .Select(b => new BookServiceModel
                 {
                     Name = b.Name,
                     AuthorName = b.AuthorName,
                     Price = b.Price,
                     Year = b.Year
                 })
                 .ToListAsync();

        public async Task<bool> UpdateBook(int id, BookServiceModel book)
        {
            var bookToUpdate = await IsBookExisting(id);

            if (bookToUpdate == null)
            {
                return false;
            }

            bookToUpdate.Name = book.Name;
            bookToUpdate.AuthorName = book.AuthorName;
            bookToUpdate.Year = book.Year;
            bookToUpdate.Price = book.Price;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<BookServiceModel> DeleteBook(int id)
        {
            var bookToDelete = await IsBookExisting (id);

            this.data.Remove(bookToDelete);
            await this.data.SaveChangesAsync();

            var bookToReturn = new BookServiceModel
            {
                Name = bookToDelete.Name,
                AuthorName = bookToDelete.AuthorName,
                Year = bookToDelete.Year,
                Price = bookToDelete.Price
            };

            return bookToReturn;
        }

        private async Task<int> GetRegisteredBookId(string bookName, string authorName) 
            => await this.data
                .Books
                .Where(b =>
                    b.Name == bookName &&
                    b.AuthorName == authorName)
                .Select(b => b.Id)
                .FirstOrDefaultAsync();

        private async Task<Book> IsBookExisting(int id)
            => await this.data
                .Books
                .FirstOrDefaultAsync(b => b.Id == id);
    }
}
