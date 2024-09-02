using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UiValidation.Models;
using UiValidation.Utilities;

namespace UiValidation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Librarian librarian;

        public HomeController(ILogger<HomeController> logger, Librarian librarian)
        {
            _logger = logger;
            this.librarian = librarian;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(librarian.Books);
        }

        [HttpGet]
        public IActionResult Book()
        {
            var model = new BookViewModel(new Book());
            return View(model);
        }

        [HttpPost]
        public IActionResult Book(BookViewModel model)
        {
            switch (model.Action)
            {
                case "Proceed to next step":
                    // check if a scan is needed
                    if (model.MustCheckForDuplicates)
                    {
                        return View(model);
                    }

                    model.Book.Id = Guid.NewGuid();
                    librarian.Books.Add(model.Book);
                    return RedirectToAction("List");
                case "Check for duplicates":
                    model = new BookViewModel(model.Book);
                    model.IsDuplicate = librarian.IsDuplicate(model.Book);
                    return View(model);
                default:
                    break;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var match = librarian.Books.Find(b => b.Id == id);
            if (match != null) librarian.Books.Remove(match);
            return RedirectToAction("List");
        }
    }
}
