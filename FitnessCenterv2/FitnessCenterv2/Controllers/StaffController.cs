using FitnessCenterv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterv2.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {

        FitnessCenterEntities db = new FitnessCenterEntities();
        /// <summary>
        /// displays a page of customer list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        /// <summary>
        /// This methods opens a form page which Staff can add Customer informations.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddCustomer()
        {

            return View();

        }
        /// <summary>
        /// This method adds the given information to the database.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
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
                TempData["Success"] = u.FirstName + " " + u.LastName + " added successfully.";
                return RedirectToAction("ManageCustomer", "Staff");
            }
            return View();
        }
        /// <summary>
        /// This method opens a page that Staff can edit ,add  or delete customers.
        /// </summary>
        /// <returns></returns>
 
        [HttpGet]
        public ActionResult ManageCustomer()
        {

            return View(db.Customers.ToList());
        }
        /// <summary>
        /// This method opens a page that Staff can edit customer information.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            Customer c = db.Customers.Where(x => x.ID == id).FirstOrDefault();
            return View(c);
        }
        /// <summary>
        ///  This method updates the information from the selected customer.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
 
        [HttpPost]
        public ActionResult EditCustomer(Customer c)
        {
            if (ModelState.IsValid)
            {
                var toBeUpdated = db.Customers.Find(c.ID); ;
                db.Entry(toBeUpdated).CurrentValues.SetValues(c);
                db.SaveChanges();
                TempData["Success"] = c.FirstName + " " + c.LastName + " edited successfully.";
                return RedirectToAction("ManageCustomer", "Staff");
            }
            return View(c);
        }

        /// <summary>
        /// This method opens a page that Staff can delete Customers.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            Customer Customer = db.Customers.Where(x => x.ID == id).FirstOrDefault();
            return View(Customer);
        }
        /// <summary>
        /// This method deletes the selected customer from the list and updates the database.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCustomer(Customer s)
        {
            if (ModelState.IsValid)
            {
                using (FitnessCenterEntities fe = new FitnessCenterEntities())
                {
                    s.EMail = fe.Staffs.Where(x => x.ID == s.ID).FirstOrDefault().EMail;
                    User u = fe.Users.AsNoTracking().Single(x => x.EMail == s.EMail);
                    fe.Users.Attach(u);
                    fe.Users.Remove(u);
                }

                db.Customers.Attach(s);
                db.Customers.Remove(s);
                db.SaveChanges();
                TempData["Success"] = s.FirstName + " " + s.LastName + " deleted successfully.";
                return RedirectToAction("ManageCustomer", "Staff");
            }
            return View();
        }

        /// <summary>
        /// This method opens a page that Staff can add, delete and update Trainers schedule.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetTrainerSchedule()
        {
            return View(db.TrainerSchedules.ToList());
        }
        /// <summary>
        /// This method opens a page that Staff can fill a form about Trainers schedule.
        /// </summary>
        /// <returns></returns>
 
        [HttpGet]
        public ActionResult AddTrainerSchedule()
        {

            ViewBag.Types = new SelectList(db.TypeTables, "ID", "Type");
            ViewBag.Trainers = new SelectList(db.Trainers, "ID", "FirstName");
            return View();
        }
        /// <summary>
        /// This method adds the given information to the database.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="selectedID"></param>
        /// <param name="selectedIDforType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddTrainerSchedule(TrainerSchedule t, String selectedID,String selectedIDforType)
        {

            if (ModelState.IsValid)
            {
                t.TrainerID = Convert.ToInt32(selectedID);
                t.Type = Convert.ToInt32(selectedIDforType);
                db.TrainerSchedules.Add(t);
                db.SaveChanges();
                TempData["Success"] = "Schedule added successfully.";
                return RedirectToAction("SetTrainerSchedule", "Staff");
            }
            return View();
        }

        /// <summary>
        ///  This method opens a form page that Staff can edit Trainers Schedule.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditSchedule(int id)
        {
            ViewBag.Types = new SelectList(db.TypeTables, "ID", "Type",id);
            ViewBag.Trainers = new SelectList(db.Trainers, "ID", "FirstName",id);
            TrainerSchedule c = db.TrainerSchedules.Where(x => x.ID == id).FirstOrDefault();
            return View(c);
        }
        /// <summary>
        /// This method adds the given information to the database.        /// </summary>
        /// <param name="t"></param>
        /// <param name="selectedID"></param>
        /// <param name="selectedIDforType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSchedule(TrainerSchedule t, String selectedID,String selectedIDforType)
        {
            if (ModelState.IsValid)
            {
                TypeTable type = db.TypeTables.Find(Convert.ToInt32(selectedIDforType));
                t.TrainerID = Convert.ToInt32(selectedID);
                TrainerSchedule ts = new TrainerSchedule();
                ts.ID = t.ID;
                ts.TrainerID = Convert.ToInt32(selectedID);
                ts.Type = type.ID;
                ts.Time = t.Time;
                var toBeUpdated = db.TrainerSchedules.Where(x => x.ID == t.ID).FirstOrDefault();
                db.Entry(toBeUpdated).CurrentValues.SetValues(ts);
                db.SaveChanges();
                TempData["Success"] = "Schedule edited successfully.";
                return RedirectToAction("SetTrainerSchedule", "Staff");
            }
            return View(t);
        }
        /// <summary>
        /// This method opens a confirmation page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteTrainerSchedule(int id)
        {
            TrainerSchedule ts = db.TrainerSchedules.Where(x => x.ID == id).FirstOrDefault();
            return View(ts);
        }
        /// <summary>
        /// This method deletes the selected Schedule from the list and updates the database..
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteTrainerSchedule(TrainerSchedule s)
        {

            if (ModelState.IsValid)
            {
                db.TrainerSchedules.Attach(s);
                db.TrainerSchedules.Remove(s);
                db.SaveChanges();
                TempData["Success"] = "Schedule deleted successfully.";
                return RedirectToAction("ManageCustomer", "Staff");
            }
            return View();
        }

        /// <summary>
        /// This method opens a form that Staff can generate report.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GenerateReport()
        {
            ViewBag.Managers = new SelectList(db.Managers, "ID", "FirstName");
            return View();

        }
        /// <summary>
        /// This method sends generated report to the selected Manager.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="selectedID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GenerateReport(Report r, String selectedID)
        {
            if (ModelState.IsValid)
            {
                r.SendDate = DateTime.Now;

                var id = int.Parse(Session["UserID"].ToString());
                User first = db.Users.Where(x => x.UserID == id).FirstOrDefault();
                Staff f = db.Staffs.Where(x => x.EMail == first.EMail).FirstOrDefault();

                r.SenderID = f.ID;
                r.ReceiverID = Convert.ToInt32(selectedID);

                db.Reports.Add(r);
                db.SaveChanges();

                User u = db.Users.Where(x => x.UserID == r.ReceiverID).FirstOrDefault();

                GMailer.GmailUsername = "se301morgoth@gmail.com";
                GMailer.GmailPassword = "301mm301";

                GMailer mailer = new GMailer();
                mailer.ToEmail = u.EMail;
                mailer.Subject = r.Subject;
                mailer.Body = r.Body;
                mailer.IsHtml = true;
                mailer.Send();
                ViewBag.Messsage = "Your report has been sent.";
                return RedirectToAction("ManageCustomer", "Staff");
            }
            return View();
        }

        /// <summary>
        /// This method opens a page of guest list.
        /// </summary>
        /// <returns></returns>
        public ActionResult GuestList()
        {
            return View(db.GuestLists.ToList());
        }
 
    }
}