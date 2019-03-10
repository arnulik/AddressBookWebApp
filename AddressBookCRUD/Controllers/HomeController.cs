using AddressBookCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressBookCRUD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPeople()
        {
            using (AddressBookDatabaseEntities addressBookDBEntities = new AddressBookDatabaseEntities())
            {
                var people = addressBookDBEntities.People.OrderBy(a => a.FirstName).ToList();
                return Json(new { data = people }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (AddressBookDatabaseEntities addressBookDBEntities = new AddressBookDatabaseEntities())
            {
                var person = addressBookDBEntities.People.Where(a => a.Id == id).FirstOrDefault();
                return View(person);
            }
        }

        [HttpPost]
        public ActionResult Save(Person person)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (AddressBookDatabaseEntities addressBookDBEntities = new AddressBookDatabaseEntities())
                {
                    if (person.Id > 0)
                    {
                        //Edit person if exists
                        var personToEdit = addressBookDBEntities.People.Where(a => a.Id == person.Id).FirstOrDefault();
                        if  (personToEdit != null)
                        {
                            personToEdit.FirstName = person.FirstName;
                            personToEdit.LastName = person.LastName;
                            personToEdit.SecondName = person.SecondName;
                            personToEdit.BirthDate = person.BirthDate;
                            personToEdit.Description = person.Description;
                            personToEdit.Email = person.Email;
                        }
                    }
                    else
                    {
                        //Add new person
                        addressBookDBEntities.People.Add(person);
                    }
                    addressBookDBEntities.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (AddressBookDatabaseEntities addressBookDBEntities = new AddressBookDatabaseEntities())
            {
                var personToDelete = addressBookDBEntities.People.Where(a => a.Id == id).FirstOrDefault();
                if (personToDelete != null)
                {
                    return View(personToDelete);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePerson(int id)
        {
            bool status = false;

            using (AddressBookDatabaseEntities addressBookDBEntities = new AddressBookDatabaseEntities())
            {
                var personToDelete = addressBookDBEntities.People.Where(a => a.Id == id).FirstOrDefault();
                if (personToDelete != null)
                {
                    addressBookDBEntities.People.Remove(personToDelete);
                    addressBookDBEntities.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}