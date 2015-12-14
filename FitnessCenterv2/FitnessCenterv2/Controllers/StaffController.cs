using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterv2.Controllers
{
<<<<<<< HEAD
    [Authorize(Roles="Staff")]
=======
    [Authorize(Roles = "Staff")]
>>>>>>> master
    public class StaffController : Controller
    {

        FitnessCenterEntities db = new FitnessCenterEntities();
        // GET: Staff
        public ActionResult Index()
        {
            return View( );
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddCustomer(Customer t)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(t);
                db.SaveChanges();

                User u = new User();
                u.EMail = t.EMail;
                u.FirstName = t.FirstName;
                u.LastName = t.LastName;
                u.Password = t.Password;
                u.Role = "Staff";
                db.Users.Add(u);
                db.SaveChanges();

                return RedirectToAction("ManageCustomer", "Staff");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ManageCustomer()
        {

            return View(db.Customers.ToList());
        }

        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            Customer c = db.Customers.Where(x => x.ID == id).FirstOrDefault();
            return View(c);
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer c)
        {
            if (ModelState.IsValid)
            {
                var toBeUpdated = db.Customers.Find(c.ID);;
                db.Entry(toBeUpdated).CurrentValues.SetValues(c);
                db.SaveChanges();
                return RedirectToAction("ManageCustomer", "Staff");
            }
            return View(c);
        }


        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            Customer Customer = db.Customers.Where(x => x.ID == id).FirstOrDefault();
            return View(Customer);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(Customer s)
        {

            //USER DANDA SİL
            if (ModelState.IsValid)
            {
                db.Customers.Attach(s);
                db.Customers.Remove(s);
                db.SaveChanges();
                return RedirectToAction("ManageCustomer", "Staff");
            }
            return View();
        }

    }
}