using UiValidation.Models;

namespace UiValidation.Utilities
{
    public class Librarian
    {
        public List<Book> Books { get; private set; } = new List<Book>();
        public bool IsDuplicate(Book book)
        {
            return Books.Any(b => b.Title == book.Title && b.GenreId == book.GenreId);
        }
    }
}
