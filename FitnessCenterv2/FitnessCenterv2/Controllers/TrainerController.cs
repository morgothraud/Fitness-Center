﻿using FitnessCenterv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterv2.Controllers
{
    [Authorize(Roles="Trainer")]
    public class TrainerController : Controller
    {

        FitnessCenterEntities db = new FitnessCenterEntities();
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getCustomers()
        {
            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Trainer t = db.Trainers.Where(x=>x.EMail == u.EMail).FirstOrDefault();
           
            return View(db.TrainerCustomerATables.Where(x=>x.TrainerID == t.ID).ToList());
        }


        [HttpGet]
        public ActionResult ManagePrograms()
        {

            return View(db.WorkoutPrograms.ToList());
        }

        [HttpGet]
        public ActionResult AddWorkoutSchedule()
        {
            ViewBag.Trainers = new SelectList(db.Trainers, "ID", "FirstName");
            return View();
        }

        [HttpPost]
        public ActionResult AddWorkoutSchedule(WorkoutProgram ws,String selectedID)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
                Trainer t = db.Trainers.Where(x => x.EMail == u.EMail).FirstOrDefault();
                ws.TrainerID = t.ID;
                db.WorkoutPrograms.Add(ws);
                db.SaveChanges();
                TempData["Success"] = "Workout added successfully.";
                return RedirectToAction("ManagePrograms", "Trainer");
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditWorkoutSchedule(int id)
        {
            WorkoutProgram wp = db.WorkoutPrograms.Where(x => x.ID == id).FirstOrDefault();
            return View(wp);
        }

        [HttpPost]
        public ActionResult EditWorkoutSchedule(WorkoutProgram wp)
        {

            if (ModelState.IsValid)
            {
                var toBeUpdated = db.WorkoutPrograms.Find(wp.ID);
                db.Entry(toBeUpdated).CurrentValues.SetValues(wp);
                db.SaveChanges();
                TempData["Success"] = "Workout edited successfully.";
                return RedirectToAction("ManagePrograms", "Trainer");
            }

            return View(wp);
        }

        [HttpGet]
        public ActionResult DeleteWorkoutSchedule(int id)
        {
            WorkoutProgram wp = db.WorkoutPrograms.Where(x => x.ID == id).FirstOrDefault();
            return View(wp);
        }
 
        [HttpPost]
        public ActionResult DeleteWorkoutSchedule(WorkoutProgram s)
        {

            if (ModelState.IsValid)
            {
                db.WorkoutPrograms.Attach(s);
                db.WorkoutPrograms.Remove(s);
                db.SaveChanges();
                TempData["Success"] = "Workout deleted successfully.";
                return RedirectToAction("ManagePrograms", "Trainer");
            }
            return View();
        }

        [HttpGet]
        public ActionResult MySchedule()
        {
            User u = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Trainer t = db.Trainers.Where(x => x.EMail == u.EMail).FirstOrDefault();
            return View(db.TrainerSchedules.Where(x=>x.TrainerID==t.ID).ToList());
        }


        [HttpGet]
        public ActionResult MyProfile()
        {
            User first = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Trainer c = db.Trainers.Where(x => x.EMail == first.EMail).FirstOrDefault();
            return View(c);
        }

        [HttpPost]
        public ActionResult MyProfile(Trainer c)
        {
            {
                var toBeUpdated = db.Trainers.Find(c.ID);
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
                return RedirectToAction("MyProfile", "Trainer");
            }
        }
    }
}