using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FitnessCenterv2.Models;


namespace FitnessCenterv2.Controllers
{
    public class TrainerController : Controller
    {
        FitnessCenterEntities db = new FitnessCenterEntities();
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult getCustomer()
        {

            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Trainer t = db.Trainers.Where(x => x.EMail == u.EMail).FirstOrDefault();
            return View(db.TrainerCustomerATables.Where(x => x.TrainerID == t.ID).ToList());
        }
        [HttpGet]
        public ActionResult AddWorkoutSchedule()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddWorkoutSchedule(WorkoutProgram ws)
        {
            if (ModelState.IsValid)
            {
               
                User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
                Trainer t = db.Trainers.Where(x => x.EMail == u.EMail).FirstOrDefault();
                ws.TrainerID = t.ID;

                db.WorkoutPrograms.Add(ws);
                db.SaveChanges();
                return RedirectToAction("Index", "Trainer");
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditSchedule(int id)
        {
            WorkoutProgram wp = db.WorkoutPrograms.Where(x => x.ID == id).FirstOrDefault();
            return View(wp);
        }

        [HttpPost]
        public ActionResult EditSchedule(WorkoutProgram wp)
        {

         
            if (ModelState.IsValid)
            {
                var toBeUpdated = db.WorkoutPrograms.Find(wp.ID);
                db.Entry(toBeUpdated).CurrentValues.SetValues(wp);
                db.SaveChanges();

                return RedirectToAction("Index", "Trainer");
            }

            return View(wp);
        }
        [HttpGet]
        public ActionResult GenerateReport()
        {
            ViewBag.Managers = new SelectList(db.Managers, "ID", "FirstName");
            return View();

        }
        [HttpPost]
        public ActionResult GenerateReport(Report r, String selectedID)
        {
            if (ModelState.IsValid)
            {
                r.SendDate = DateTime.Now;
                var id = int.Parse(Session["UserID"].ToString());
                User first = db.Users.Where(x => x.UserID == id).FirstOrDefault();
                Trainer f = db.Trainers.Where(x => x.EMail == first.EMail).FirstOrDefault();

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
                return RedirectToAction("Index", "Trainer");

            }

            return View();
        }




    }
}

