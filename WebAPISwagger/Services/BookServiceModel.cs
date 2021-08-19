using System.Text.Json.Serialization;

namespace WebAPISwagger.Services
{
    public class BookServiceModel
    {
        public string Name { get; init; }

        public string AuthorName { get; init; }

        public int Year { get; init; }

        public decimal Price { get; init; }
    }
}
