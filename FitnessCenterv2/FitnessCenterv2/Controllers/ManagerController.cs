﻿using FitnessCenterv2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterv2.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {

        FitnessCenterEntities db = new FitnessCenterEntities();
        /// <summary>
        /// This method displays a list of staffs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        /// <summary>
        ///  This methods opens a form page which Manager can add Staff informations.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddStaff()
        {

            return View();

        }
        /// <summary>
        /// This method adds the given Staff information to the database.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddStaff(Staff t)
        {
            User search = db.Users.Where(x => x.EMail == t.EMail).FirstOrDefault();

            if (ModelState.IsValid && search == null)
            {
                t.HireDate = DateTime.Now;
                db.Staffs.Add(t);
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
                return RedirectToAction("ManageStaff", "Manager");

            }
            if (search != null)
                TempData["Error"] = search.EMail + " is already registered to the system.";
            return View();
        }

        /// <summary>
        /// This method opens a page that Manager can edit ,add  or delete Staff.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageStaff()
        {

            return View(db.Staffs.ToList());
        }

        /// <summary>
        /// This method opens a page that Manager can edit Staff information.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditStaff(int id)
        {
            Staff staff = db.Staffs.Where(x => x.ID == id).FirstOrDefault();
            return View(staff);
        }
        /// <summary>
        ///  This method updates the information from the selected Staff.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditStaff(Staff s)
        {
            if (ModelState.IsValid)
            {
                User search = db.Users.Where(x => x.EMail == s.EMail).FirstOrDefault();
                Staff sys = db.Staffs.Where(x => x.EMail == s.EMail).FirstOrDefault();
                if (s.Equals(sys)) return RedirectToAction("ManageStaff", "Manager");

                else if (search != null)
                {
                    TempData["Error"] = search.EMail + " is already registered to the system.";
                    return View();
                }
                else if (search != null && search.EMail == s.EMail)
                {
                    var toBeUpdatedfromUsers = db.Users.Where(x => x.EMail == s.EMail).FirstOrDefault();
                    var toBeUpdated = db.Staffs.Find(s.ID);
                    db.Entry(toBeUpdated).CurrentValues.SetValues(s);
                    db.SaveChanges();

                    using (FitnessCenterEntities ff = new FitnessCenterEntities())
                    {
                        toBeUpdatedfromUsers.EMail = s.EMail;
                        toBeUpdatedfromUsers.FirstName = s.FirstName;
                        toBeUpdatedfromUsers.LastName = s.LastName;
                        toBeUpdatedfromUsers.Password = s.Password;
                        ff.Entry(toBeUpdatedfromUsers).CurrentValues.SetValues(toBeUpdatedfromUsers);
                    }
                    TempData["Success"] = s.FirstName + " " + s.LastName + " is edited successfully.";
                    return RedirectToAction("ManageStaff", "Manager");
                }
            }
            return View();
        }
        /// <summary>
        /// This method opens a page that Manager can delete Staff.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteStaff(int id)
        {
            Staff staff = db.Staffs.Where(x => x.ID == id).FirstOrDefault();
            return View(staff);
        }


        /// <summary>
        /// This method deletes the selected Staff from the list and updates the database.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult DeleteStaff(Staff s)
        {

            using (FitnessCenterEntities fe = new FitnessCenterEntities())
            {
                s.EMail = fe.Staffs.Where(x => x.ID == s.ID).FirstOrDefault().EMail;

                User u = fe.Users.Where(x => x.EMail == s.EMail).FirstOrDefault();
                if (u != null)
                {
                    fe.Users.Attach(u);
                    fe.Users.Remove(u);
                    fe.SaveChanges();

                }

            }

            //birden ço kreport u olursa hepsini silmek lazım     

            /*      using (FitnessCenterEntities fe2 = new FitnessCenterEntities())
                  {
                      Report r = db.Reports.Find(s.ID);
                      if (r != null)
                      {
                         // ((IObjectContextAdapter)fe2).ObjectContext.Detach(s);
                          fe2.Reports.Attach(db.Reports.Where(x=>x.SenderID == s.ID).FirstOrDefault());
                          fe2.Reports.Remove(r);
                          fe2.SaveChanges();
                   
                      }
                  }*/

            Report r = db.Reports.Where(x => x.SenderID == s.ID).FirstOrDefault();
            if (r != null)
            {
                db.Reports.Attach(r);
                db.Reports.Remove(r);
                db.SaveChanges();
            }
            db.Staffs.Attach(s);
            db.Staffs.Remove(s);
            db.SaveChanges();
            TempData["Success"] = "Deleted successfully.";
            return RedirectToAction("ManageStaff", "Manager");


        }


        //EQUIPMENT ACTIONS

        /// <summary>
        /// This method displays the page for adding equipment.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult AddEquipment()
        {

            return View();
        }

        /// <summary>
        /// This method creates a equipment and updates the database.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEquipment(Equipment t)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Add(t);
                db.SaveChanges();
                TempData["Success"] = t.Name + " added successfully.";
                return RedirectToAction("ManageEquipment", "Manager");
            }
            return View();
        }

        /// <summary>
        /// This method displays a list of equipments where you can edit equipments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageEquipment()
        {

            return View(db.Equipments.ToList());
        }

        /// <summary>
        /// This method gets the page where you can edit the equipment with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditEquipment(int id)
        {
            Equipment equipment = db.Equipments.Where(x => x.ID == id).FirstOrDefault();
            return View(equipment);
        }

        /// <summary>
        /// This method edits the equipment from the database.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditEquipment(Equipment s)
        {
            if (ModelState.IsValid)
            {
                var toBeUpdated = db.Equipments.Find(s.ID);

                db.Entry(toBeUpdated).CurrentValues.SetValues(s);
                db.SaveChanges();
                TempData["Success"] = s.Name + " edited successfully.";
                return RedirectToAction("ManageEquipment", "Manager");
            }

            return View(s);
        }

        /// <summary>
        /// This method displays a page where if user wants to delete the equipment with the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteEquipment(int id)
        {
            Equipment equipment = db.Equipments.Where(x => x.ID == id).FirstOrDefault();
            return View(equipment);
        }

        /// <summary>
        /// This method deletes the equipment from the database.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteEquipment(Equipment s)
        {

            db.Equipments.Attach(s);
            db.Equipments.Remove(s);
            db.SaveChanges();
            TempData["Success"] = s.Name + " deleted successfully.";
            return RedirectToAction("ManageEquipment", "Manager");


        }

        /// <summary>
        /// This method opens a page of the Managers current report messages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReportList()
        {

            int currentUserID = int.Parse(Session["UserID"].ToString());
            List<Report> reportList = db.Reports.Where(x => x.ReceiverID == currentUserID).ToList();

            //   Staff firstName = db.Staffs.FirstOrDefault(x => x.ID == r.SenderID);

            ViewBag.Reports = new SelectList(reportList, "ID", "SenderID");
            ViewBag.Staffs = new SelectList(db.Staffs, "ID", "FirstName");

            var ent = (from r in db.Reports
                       join s in db.Staffs on r.SenderID equals s.ID
                       where r.SenderID == s.ID
                       select new
                       {
                           r.Body,
                           r.Subject,
                           s.FirstName
                       }).ToList();

            ViewBag.test = new SelectList(ent, "Body", "FirstName");

            return View(reportList);
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            User first = db.Users.Find(int.Parse(Session["UserID"].ToString()));
            Manager c = db.Managers.Where(x => x.EMail == first.EMail).FirstOrDefault();
            return View(c);
        }

        [HttpPost]
        public ActionResult MyProfile(Manager c)
        {
            {
                var toBeUpdated = db.Managers.Find(c.ID);
                toBeUpdated.Address = c.Address;
                toBeUpdated.Password = c.Password;
                toBeUpdated.Phone = c.Phone;
                toBeUpdated.City = c.City;
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


        }
    }
}