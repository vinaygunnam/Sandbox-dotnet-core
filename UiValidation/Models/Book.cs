using System.ComponentModel.DataAnnotations;

namespace UiValidation.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please select a genre.")]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }
        [MinLength(1, ErrorMessage = "Please select at least one country to publish the title in.")]
        public string[] Countries { get; set; }
        public override string ToString()
        {
            var detail = new[] { 
                Title, 
                GenreId?.ToString(), 
                ((Countries?.Length ?? 0) > 0) ? String.Join('_', Countries) : "",
            };
            return String.Join(":::", detail);
        }
    }
}
