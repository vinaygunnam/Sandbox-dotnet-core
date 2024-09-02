namespace UiValidation.Models
{
    public class BookViewModel
    {
        public Book Book { get; set; } = new Book();
        public string? Action { get; set; }
        public string DataThumbprintFromLastScan { get; set; } = string.Empty;
        public bool IsDuplicate { get; set; }
        public bool MustCheckForDuplicates
        {
            get
            {
                return this.DataThumbprintFromLastScan != this.Book.ToString();
            }
        }
        public BookViewModel()
        {

        }

        public BookViewModel(Book book)
        {
            this.Book = book ?? new Book();
            this.DataThumbprintFromLastScan = this.Book.ToString();
        }
    }
}
