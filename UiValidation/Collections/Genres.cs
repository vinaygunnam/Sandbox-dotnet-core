using Microsoft.AspNetCore.Mvc.Rendering;

namespace UiValidation.Collections
{
    public class Genres
    {
        public static Dictionary<int, string> List = new()
        {
            { 1, "Children" },
            { 2, "Fantasy" },
            { 3, "Mystery" },
            { 4, "Science Fiction" }
        };

        public static SelectList Selection  = new SelectList(List, "Key", "Value");
    }
}
