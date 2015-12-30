using FitnessCenterv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FitnessCenterv2.Controllers
{

    public class CustomerController : Controller
    {

        FitnessCenterEntities db = new FitnessCenterEntities();
        // GET: Customer
        [Authorize(Roles = "Customer")]
        public ActionResult Index()
        {
            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Customer c = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();

            DateTime dt = Convert.ToDateTime(c.EndDateMemberShip);
            TimeSpan difference = dt - DateTime.Now;
            ViewBag.RemaningTime = difference.Days.ToString();
            return View();
        }

        [HttpGet, Authorize(Roles = "Customer,Expired")]
        public ActionResult refreshMemberShip()
        {

            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Customer c = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();
            return View(c);
        }



        [HttpPost, Authorize(Roles = "Customer,Expired")]
        public ActionResult refreshMemberShip(Customer c)
        {

            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Customer cust = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();

            if (cust.CreditCardNumber == null)
            {
                TempData["Error"] = "You don't have a registered credit card to the system." + cust.EndDateMemberShip.ToString();
                return View("refreshMemberShip", cust);

            }

            DateTime dt = new DateTime();
            if (Convert.ToDateTime(cust.EndDateMemberShip) < DateTime.Now)
            {
                dt = DateTime.Now;
            }
            else dt = Convert.ToDateTime(cust.EndDateMemberShip);
            dt = dt.AddDays(Convert.ToDouble(c.MemberShipDays));
            cust.EndDateMemberShip = dt;
            cust.MemberShipDays = c.MemberShipDays;
            TempData["Success"] = "Your membership has been extended 'till " + cust.EndDateMemberShip.ToString();
            var toBeUpdated = db.Customers.Find(c.ID);
            db.Entry(toBeUpdated).CurrentValues.SetValues(cust);
            db.SaveChanges();

            if (u.Role == "Expired")
            {
                u.Role = "Customer";
                db.Entry(u).CurrentValues.SetValues(u);
                db.SaveChanges();
                TempData["PaymentSuccess"] = "Your payment is successful. You may login now.";
                FormsAuthentication.SignOut();
                Session.Clear();
                return RedirectToAction("Login", "Account");
            }
            /*  DateTime remaining = Convert.ToDateTime(c.EndDateMemberShip);
              String s = "Your membership will end on" + remaining.ToString();*/
            return RedirectToAction("Index", "Customer");

        }

        [Authorize(Roles = "Customer,Expired")]
        [HttpGet]
        public ActionResult ChangeCardNumber(int id)
        {

            Customer c = db.Customers.Where(x => x.ID == id).FirstOrDefault();
            if (c.CreditCardNumber != null)
                TempData["Info"] = c.CreditCardNumber + " is registered to our system. You may delete your credit card info below if you like.";
            else TempData["Info"] = "Your do not have a registered credit card to our systems at the moment.";
            return View(c);
        }

        [Authorize(Roles = "Customer,Expired")]
        [HttpPost]
        public ActionResult ChangeCardNumber(Customer c)
        {
            if (ModelState.IsValidField("CreditCardNumber"))
            {
                Customer current = db.Customers.Find(c.ID);
                current.CreditCardNumber = c.CreditCardNumber;
                db.Entry(current).CurrentValues.SetValues(current);
                db.SaveChanges();
                TempData["Success"] = "Your credit card info has been successfully updated.";
                return RedirectToAction("refreshMemberShip", "Customer");
            }
            return View();
        }




        [Authorize(Roles = "Customer")]
        public ActionResult SelectProgram()
        {
            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Customer c = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();
            WorkoutProgram w = db.WorkoutPrograms.Find(c.WorkoutID);

            if (w != null)
                if (w.Schedule.Length > 10)
                    TempData["Info"] = "Your current program is " + w.Schedule.Substring(0, w.Schedule.Length - 5) + "...";
                else TempData["Info"] = "Your current program is " + w.Schedule;
            else TempData["Info"] = "You do not have a current schedule.";
            return View(db.WorkoutPrograms.ToList());
        }
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public ActionResult BringGuest()
        {
            return View();
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult BringGuest(GuestList g)
        {
            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Customer c = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();

            GuestList current = db.GuestLists.Where(x => x.CustomerID == c.ID).FirstOrDefault();
            if (current != null)
            {
                DateTime d = current.Date ?? DateTime.Now;

                d = d.AddDays(6);
                if (d <= DateTime.Now)
                {
                    g.CustomerID = c.ID;
                    db.GuestLists.Add(g);
                    db.SaveChanges();
                    TempData["Success"] = g.GuestFirstName + " " + g.GuestLastName + " is now your guest.";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    TempData["Error"] = "You can only bring one guest per week. You brought " + current.Customer.FirstName + " " +
                        current.Customer.LastName + " this week. You can bring your guest back again " + d;
                    return View();
                }

            }
            else if (current == null)
            {
                g.CustomerID = c.ID;
                db.GuestLists.Add(g);
                db.SaveChanges();
                TempData["Success"] = g.Customer.FirstName + " " + g.Customer.LastName + " is now your guest.";
                ModelState.Clear();
                return View();
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public ActionResult ScheduleDetails(int id)
        {
            WorkoutProgram w = db.WorkoutPrograms.Find(id);
            return View(w);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult ScheduleDetails(WorkoutProgram w)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
                Customer c = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();
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

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public ActionResult MyProfile()
        {
            User first = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Customer c = db.Customers.Where(x => x.EMail == first.EMail).FirstOrDefault();
            return View(c);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult MyProfile(Customer c)
        {
            if (ModelState.IsValid) { 
            var toBeUpdated = db.Customers.Find(c.ID);
            toBeUpdated.Address = c.Address;
            toBeUpdated.Password = c.Password;
            toBeUpdated.Phone = c.Phone;
            db.Entry(toBeUpdated).CurrentValues.SetValues(toBeUpdated);
            db.SaveChanges();

            using (FitnessCenterEntities ff = new FitnessCenterEntities())
            {

                User first = ff.Users.Find(int.Parse(Session["UserID"].ToString()));
                first.Password = c.Password;
                ff.Entry(first).CurrentValues.SetValues(first);
                ff.SaveChanges();

            }
            return RedirectToAction("MyProfile", "Customer");
            }
            User uFirst = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Customer fCust = db.Customers.Where(x => x.EMail == uFirst.EMail).FirstOrDefault();
            return View(fCust);
        }



        [HttpGet]
        [Authorize(Roles = "Customer")]
        public ActionResult TrainerList()
        {
            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Customer c = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();
            TrainerCustomerATable t = db.TrainerCustomerATables.Where(x=>x.CustomerID==c.ID).FirstOrDefault();
            if (t != null)
            {
                TempData["Info"] = "Your trainer is " + t.Trainer.FirstName + " " + t.Trainer.LastName;
            }
            else if (t == null)
            {
                TempData["Info"] = "Your do not have a current trainer. You can choose one below.";
            }
            return View(db.Trainers.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public ActionResult TrainerDetails(int id)
        {
            Trainer w = db.Trainers.Find(id);
            return View(w);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult TrainerDetails(Trainer w)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
                Customer c = db.Customers.Where(x => x.EMail == u.EMail).FirstOrDefault();
                TrainerCustomerATable t = db.TrainerCustomerATables.Where(x => x.CustomerID == c.ID).FirstOrDefault();

                if (t == null)
                {
                    TrainerCustomerATable trainerCustomer = new TrainerCustomerATable();
                    trainerCustomer.CustomerID = c.ID;
                    trainerCustomer.TrainerID = w.ID;
                    db.TrainerCustomerATables.Add(trainerCustomer);
                    db.SaveChanges();
                    Trainer final = db.Trainers.Find(w.ID);
                    TempData["Success"] = "Your trainer is now " + final.FirstName + " " + final.LastName;
                    return RedirectToAction("TrainerList", "Customer");
                }
                else
                {
                    t.TrainerID = w.ID;
                    db.Entry(t).CurrentValues.SetValues(t);
                    db.SaveChanges();
                    Trainer final = db.Trainers.Find(w.ID);
                    TempData["Success"] = "Your trainer is now " + final.FirstName + " " + final.LastName;
                    return RedirectToAction("TrainerList", "Customer");
                }
            }
            return View();
        }
    }
}