using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;
using PeopleManager.Ui.Mvc.Models;
using System;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly PeopleManagerDbContext _peopleManagerDbContext;
        public OrganizationsController(PeopleManagerDbContext peopleManagerDbContext)
        {
            _peopleManagerDbContext = peopleManagerDbContext;
        }

        public ActionResult Index()
        {
            var organization = _peopleManagerDbContext.Organizations.ToList();
            return View(organization);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return View(organization);
            }

            _peopleManagerDbContext.Organizations.Add(organization);
            _peopleManagerDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var organization = _peopleManagerDbContext.Organizations
                .FirstOrDefault(o => o.Id == id); 

            if (organization == null)
            {
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, [FromForm] Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return View(organization);
            }

            var dbOrganization = _peopleManagerDbContext.Organizations
                .FirstOrDefault(o => o.Id == id);

            if (dbOrganization == null)
            {
                return RedirectToAction("Index");
            }

            // Mapping
            dbOrganization.Name = organization.Name;
            dbOrganization.Description = organization.Description;

            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var organization = _peopleManagerDbContext.Organizations
                .FirstOrDefault(o => o.Id == id);

            if (organization == null)
            {
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        [HttpPost("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var dbOrganization = _peopleManagerDbContext.Organizations
                .FirstOrDefault(o => o.Id == id);

            if (dbOrganization == null)
            {
                return RedirectToAction("Index");
            }

            _peopleManagerDbContext.Organizations.Remove(dbOrganization);
            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
