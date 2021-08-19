using System.Collections.Generic;

namespace WebAPISwagger.Services
{
    public interface IBookService
    {
        void CreateBook(BookServiceModel book);

        //BookServiceModel GetBook(int id);

        IEnumerable<BookServiceModel> GetBooks(int id = 0);

        void UpdateBook(int id, BookServiceModel book);

        bool DeleteBook(int id);
    }
}
