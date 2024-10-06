namespace UiValidation.Models
{
    public class DateTimeMatcher
    {
        public Int16? Day { get; set; }
        public Int16? Month { get; set; }
        public Int16? Year { get; set; }
        public bool UsePartialMatch { get; set; }
    }
}
