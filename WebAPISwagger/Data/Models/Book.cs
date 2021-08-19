using System.ComponentModel.DataAnnotations;
using static WebAPISwagger.Data.DataConstants;

namespace WebAPISwagger.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(BookNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(AuthorNameMaxLength)]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }
    }
}
