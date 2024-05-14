using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Core;
using PeopleManager.Ui.Mvc.Models;
using System.Diagnostics;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly PeopleManagerDbContext _peopleManagerDbContext;
        public HomeController(PeopleManagerDbContext peopleManagerDbContext)
        {
            _peopleManagerDbContext = peopleManagerDbContext;
        }

        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            var people = _peopleManagerDbContext.People
                .Include(person => person.Organization)
                .ToList();
            return View(people);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /* >>> now in the Database
        private IList<Person> GetPeople()
        {
            return new List<Person>
        {
            new Person { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" },
            new Person { FirstName = "Jane", LastName = "Smith" }, // Email not specified
            new Person { FirstName = "Michael", LastName = "Johnson", Email = "michaeljohnson@example.com" },
            new Person { FirstName = "Emily", LastName = "Jones" }, // Email not specified
            new Person { FirstName = "Chris", LastName = "Brown", Email = "chrisbrown@example.com" },
            new Person { FirstName = "David", LastName = "Wilson" }, // Email not specified
            new Person { FirstName = "Olivia", LastName = "Taylor", Email = "oliviataylor@example.com" },
            new Person { FirstName = "Daniel", LastName = "Moore" }, // Email not specified
            new Person { FirstName = "Amanda", LastName = "Anderson", Email = "amandaanderson@example.com" },
            new Person { FirstName = "James", LastName = "Thomas", Email = "jamesthomas@example.com" }
        };
        }*/
    }
}
