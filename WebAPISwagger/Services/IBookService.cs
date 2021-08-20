using System.Collections.Generic;

namespace WebAPISwagger.Services
{
    public interface IBookService
    {
        int RegisterBook(BookServiceModel book);

        BookServiceModel GetBook(int id);

        IEnumerable<BookServiceModel> GetBooks();

        bool UpdateBook(int id, BookServiceModel book);

        BookServiceModel DeleteBook(int id);
    }
}
