using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterv2.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        int ScheduleID;

        FitnessCenterEntities db = new FitnessCenterEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult SelectProgram()
        {
            return View(db.WorkoutPrograms.ToList());
        }

        [HttpGet]
        public ActionResult BringGuest()
        {

            return View();
        }
        [HttpPost]
        public ActionResult BringGuest(GuestList g)
        {
            g.CustomerID = int.Parse(Session["UserID"].ToString());
            db.GuestLists.Add(g);
            db.SaveChanges();
            return View();
        }



        [HttpGet]
        public ActionResult ScheduleDetails(int id)
        {
            WorkoutProgram w = db.WorkoutPrograms.Find(id);
            return View(w);
        }

        [HttpPost]
        public ActionResult ScheduleDetails(WorkoutProgram w)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
                Customer c = db.Customers.Where(x=>x.EMail == u.EMail).FirstOrDefault();
                var toBeUpdated = db.Customers.Find(c.ID);
                c.WorkoutID = w.ID;
                db.Entry(toBeUpdated).CurrentValues.SetValues(c);
                db.SaveChanges();
                return RedirectToAction("ManageStaff", "Customer");
            }

            return View();
        }


        public ActionResult TrainerRequests()
        {
            
            return View(w);
        }


    
    


    }
}
