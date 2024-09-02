namespace UiValidation.Models
{
    public class CheckboxList<T, U>
    {

        public Dictionary<T, U> Items { get; set; } = new Dictionary<T, U>();
        private IEnumerable<T> selectedValues;

        public IEnumerable<T> SelectedValues
        {
            get { return selectedValues; }
            set { selectedValues = value; }
        }

        public CheckboxListItem<T, U>[] ItemsToRender {
            get
            {
                return Items.Select(x => new CheckboxListItem<T, U>
                {
                    Value = x.Key,
                    Text = x.Value,
                    Selected = SelectedValues?.Contains(x.Key) ?? false,
                }).ToArray();
            }
            set
            {
                selectedValues = value.Where(x => x.Selected).Select(x => x.Value);
            }
        }
    }

    public class CheckboxListItem<T, U>
    {
        public T Value { get; set; } = default;
        public U Text { get; set; } = default;
        public bool Selected { get; set; } = false;
    }
}