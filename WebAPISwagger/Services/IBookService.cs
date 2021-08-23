using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPISwagger.Services
{
    public interface IBookService
    {
        Task<int> RegisterBook(BookServiceModel book);

        Task<BookServiceModel> GetBook(int id);

        Task<IEnumerable<BookServiceModel>> GetBooks();

        Task<bool> UpdateBook(int id, BookServiceModel book);

        Task<BookServiceModel> DeleteBook(int id);
    }
}
