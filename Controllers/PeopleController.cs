using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Core;
using PeopleManager.Ui.Mvc.Models;
using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleManagerDbContext _peopleManagerDbContext;
        public PeopleController(PeopleManagerDbContext peopleManagerDbContext)
        {
            _peopleManagerDbContext = peopleManagerDbContext;
        }

        // GET: PeopleController
        [HttpGet]
        public ActionResult Index()
        {
            /*var bavoPerson = new Person { FirstName = "Bavo", LastName = "Ketels", Email = "bavo.ketels@vives.be" };
            _database.People.Add(bavoPerson);*/

            // SELECT * FROM PEOPLE INNER JOIN ORGANIZATIONS ON Organizations.Id = People.OrganizationId
            var people = _peopleManagerDbContext.People
                .Include(person => person.Organization) // or .Include(p => p.Organization) to make it shorter
                                                        // .ThenInclude() if other things to include
                .ToList();
            return View(people);
        }

        // IList instead of List for it to be an Interface >>> now in the Database
        /*private IList<Person> GetPeople()
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

        [HttpGet]
        public IActionResult Create()
        {
            var organizations = _peopleManagerDbContext.Organizations.ToList();

            ViewBag.Organizations = organizations;
            // ViewData["Organizations"] = organizations;
            // TempData["Organizations"] = organizations; // Rarely used

            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                var organizations = _peopleManagerDbContext.Organizations.ToList();
                ViewBag.Organizations = organizations;

                return View(person);
            }

            _peopleManagerDbContext.People.Add(person);
            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet] // or [HttpGet("People/Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var person = _peopleManagerDbContext.People
                //.SingleOrDefault(person => person.Id == id);
                .FirstOrDefault(person => person.Id == id); // a little bit faster than the SingleOrDefault 
            // or .FirstOrDefault(p => p.Id == id); // equivalent to for each person in people where people.Id == id
            // |^|
            // |^| above are LINQ (Language Integrated Queries)

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            var organizations = _peopleManagerDbContext.Organizations.ToList();

            ViewBag.Organizations = organizations;

            return View(person);
        }

        [HttpPost]

        // public IActionResult Edit(int id, Person person) less specified
        public IActionResult Edit([FromRoute] int id, [FromForm] Person person)
        {
            if (!ModelState.IsValid)
            {
                var organizations = _peopleManagerDbContext.Organizations.ToList();
                ViewBag.Organizations = organizations;

                return View(person);
            }

            var dbPerson = _peopleManagerDbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (dbPerson == null)
            {
                return RedirectToAction("Index");
            }

            // Mapping
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.OrganizationId = person.OrganizationId;
            // if value overwritten is the same, no change marked
            // if value overwritten is different, Person modified and attribute modified

            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var person = _peopleManagerDbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost("[controller]/Delete/{id:int?}")]
        // [HttpPost("People/Delete/{id:int?}")]
        // [HttpPost]
        // [Route("People/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var dbPerson = _peopleManagerDbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (dbPerson == null)
            {
                return RedirectToAction("Index");
            }

            _peopleManagerDbContext.People.Remove(dbPerson);
            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
