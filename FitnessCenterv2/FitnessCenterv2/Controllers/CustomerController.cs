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
        
        FitnessCenterEntities db = new FitnessCenterEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult SelectProgram()
        {
            
                User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
                Customer c = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();
                WorkoutProgram w = db.WorkoutPrograms.Find(c.WorkoutID);
                
                if(w!=null)
                TempData["Info"] = "Your current program is " + w.Schedule.Substring(0,w.Schedule.Length-5) + "...";
                else TempData["Info"] = "You do not have a current schedule.";
          
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
                TempData["Success"] = "Workout program chosen successfully.";

                return RedirectToAction("SelectProgram", "Customer");
            }

            return View();
        }

  /*      public ActionResult ScheduleDetails(int ID)
        {
            WorkoutProgram w = db.WorkoutPrograms.Find(ID);
            ScheduleID = ID;
            return View(w);
        }


        public ActionResult AddSchedule(String s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                Customer c = db.Customers.Find(int.Parse(Session["UserID"].ToString()));
                var toBeUpdated = c;
                Customer newUpdate = c;
                newUpdate.WorkoutID = ScheduleID;
                db.Entry(toBeUpdated).CurrentValues.SetValues(s);
                db.SaveChanges();  
            }
       
            return View();
           
        }*/


    }
}