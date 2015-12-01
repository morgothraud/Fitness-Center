using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterv2.Controllers
{
    public class AccountController : Controller
    {

        FitnessCenterEntities db = new FitnessCenterEntities();
        // GET: Account
        public ActionResult Index()
        {
            
            String chart1 = "1";
            var GraphLst = new List<string> { chart1, chart1 };
            ViewBag.displayGraph = new SelectList(GraphLst);

            return View(db.Trainers.ToList());
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(User c)
        {
            if (ModelState.IsValid)
            {
                c.Role = "Customer";
                db.Users.Add(c);
 
                Customer cust = new Customer();

                cust.EMail = c.EMail;
                cust.Password = c.Password;
                cust.FirstName = c.FirstName;
                cust.LastName = c.LastName;
                db.Customers.Add(cust);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = c.FirstName + "successfully registered.";
            }
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(User c)
        {

            var user = db.Users.Where(x => x.EMail == c.EMail && x.Password == c.Password).FirstOrDefault();

            if (user != null)
            {
                if (user.Role == "Customer")
                {
                    Session["UserID"] = user.UserID.ToString();
                    Session["FirstName"] = user.FirstName.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else if (user.Role == "Manager")
                {
                    Session["UserID"] = user.UserID.ToString();
                    Session["FirstName"] = user.FirstName.ToString();
                    return RedirectToAction("LoggedInManager");
                }
            }
            else
                ModelState.AddModelError("", "Email or pw wrong.");
            return View();
        }


        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
                return View();
            else return RedirectToAction("Login");
        }


        public ActionResult LoggedInManager() {

            if (Session["UserID"] != null)
                return View(db.Trainers.ToList());
            else return RedirectToAction("Login");
        }
    }
}