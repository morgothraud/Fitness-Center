using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterv2.Controllers
{
<<<<<<< HEAD
<<<<<<< HEAD
    [Authorize(Roles = "Manager")]
=======
    [Authorize(Roles="Manager")]
>>>>>>> master
=======
    [Authorize(Roles="Manager")]
>>>>>>> master
    public class ManagerController : Controller
    {
        
        FitnessCenterEntities db = new FitnessCenterEntities();
        // GET: Manager
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        [HttpGet]
        public ActionResult AddStaff()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddStaff(Staff t)
        {
            if (ModelState.IsValid)
            {
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
                return RedirectToAction("ManageStaff", "Manager");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ManageStaff()
        {

            return View(db.Staffs.ToList());
        }


        [HttpGet]
        public ActionResult EditStaff(int id)
        {
            Staff staff = db.Staffs.Where(x => x.ID == id).FirstOrDefault();
            return View(staff);
        }

        [HttpPost]
        public ActionResult EditStaff(Staff s)
        {

            //USERDA UPDATELE
            if (ModelState.IsValid)
            {
                var toBeUpdated = db.Staffs.Find(s.ID);
                db.Entry(toBeUpdated).CurrentValues.SetValues(s);
                db.SaveChanges();

                return RedirectToAction("ManageStaff", "Manager");
            }

            return View(s);
        }

        [HttpGet]
        public ActionResult DeleteStaff(int id)
        {
            Staff staff = db.Staffs.Where(x => x.ID == id).FirstOrDefault();
            return View(staff);
        }

        [HttpPost]
        public ActionResult DeleteStaff(Staff s)
        {

            //USER DANDA SİL
            if (ModelState.IsValid)
            {
                db.Staffs.Attach(s);
                db.Staffs.Remove(s);
                db.SaveChanges();
                return RedirectToAction("ManageStaff", "Manager");
            }
            return View();
        }
        

        //EQUIPMENT ACTIONS

        [HttpGet]
        public ActionResult AddEquipment()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddEquipment(Equipment t)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Add(t);
                db.SaveChanges();
                return RedirectToAction("ManageEquipment", "Manager");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ManageEquipment()
        {

            return View(db.Equipments.ToList());
        }


        [HttpGet]
        public ActionResult EditEquipment(int id)
        {
            Equipment equipment = db.Equipments.Where(x => x.ID == id).FirstOrDefault();
            return View(equipment);
        }

        [HttpPost]
        public ActionResult EditEquipment(Equipment s)
        {
            if (ModelState.IsValid)
            {
                var toBeUpdated = db.Equipments.Find(s.ID);
                db.Entry(toBeUpdated).CurrentValues.SetValues(s);
                db.SaveChanges();

                return RedirectToAction("ManageEquipment", "Manager");
            }

            return View(s);
        }

        [HttpGet]
        public ActionResult DeleteEquipment(int id)
        {
            Equipment equipment = db.Equipments.Where(x => x.ID == id).FirstOrDefault();
            return View(equipment);
        }

        [HttpPost]
        public ActionResult DeleteEquipment(Equipment s)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Attach(s);
                db.Equipments.Remove(s);
                db.SaveChanges();
                return RedirectToAction("ManageEquipment", "Manager");
            }
            return View();
        }

    }
}