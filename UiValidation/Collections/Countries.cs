using Microsoft.AspNetCore.Mvc.Rendering;

namespace UiValidation.Collections
{
    public class Countries
    {
        public static Dictionary<string, string> List = new()
        {
            { "CA", "Canada" },
            { "FR", "France" },
            { "UK", "United Kingdom" },
            { "US", "USA" }
        };

        public static SelectList Selection = new SelectList(List, "Key", "Value");
    }
}
