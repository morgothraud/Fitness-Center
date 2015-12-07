using FitnessCenterv2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

            if (user == null)
            {
                ModelState.AddModelError("", "Email or pw wrong.");
                return View();
            }
            else {
<<<<<<< HEAD
<<<<<<< HEAD
                FormsAuthentication.SetAuthCookie(c.EMail, false);
                Session["UserID"] = user.UserID.ToString();
                Session["FirstName"] = user.FirstName.ToString();
                if (user.Role == "Manager")
                {
                    return RedirectToAction("Index", "Manager");
                }
                if (user.Role == "Staff")
                {
                    return RedirectToAction("Index", "Staff");
                }
                

=======
=======
>>>>>>> master
                FormsAuthentication.SetAuthCookie(c.EMail,false);
                Session["UserID"] = user.UserID.ToString();
                Session["FirstName"] = user.FirstName.ToString();
                if (user.Role == "Manager")
                return RedirectToAction("Index", "Manager");
                if (user.Role == "Staff")
                return RedirectToAction("Index","Staff");
<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master
            }
            return View();
        }

        #region oldLOgin
        /*  [HttpPost]
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
                    return RedirectToAction("Index", "Manager");
                }
                else if (user.Role == "Trainer")
                {


                }
                else if (user.Role == "Staff")
                {
                    Session["UserID"] = user.UserID.ToString();
                    Session["FirstName"] = user.FirstName.ToString();
                    return RedirectToAction("Index", "Staff");
                }
            }
            else
                ModelState.AddModelError("", "Email or pw wrong.");

            return View();
        }*/
        
        #endregion


        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
                return View();
            else return RedirectToAction("Login");
        }


        public ActionResult LoggedInManager()
        {

            if (Session["UserID"] != null)
                return View(db.Trainers.ToList());
            else return RedirectToAction("Login");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword(String s)
        {
            return View(s);
        }

        [HttpPost]
        public ActionResult ForgotPassword(User s)
        {
            var u = db.Users.Where(x => x.EMail == s.EMail).FirstOrDefault();
            if (u == null)
            {
                ViewBag.Message = "Such user cannot be found.";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    PassReset p = new PassReset();
                    p.AutID = Guid.NewGuid().ToString();
                    p.isAvaliable = true;
                    p.EMail = u.EMail;
                    p.UserID = u.UserID;
                    db.PassResets.Add(p);
                    db.SaveChanges();


                    GMailer.GmailUsername = "se301morgoth@gmail.com";
                    GMailer.GmailPassword = "301mm301";

                    GMailer mailer = new GMailer();
                    mailer.ToEmail = u.EMail;
                    mailer.Subject = "Password Reset";
                    mailer.Body = "A request to reset your password has been sent. <br> Please enter the code below to Change Password page to Change your Password. <br> <a href='localhost.com/Account/ChangePassword'>verify</a> Authentication Code :" + p.AutID;
                    mailer.IsHtml = true;
                    mailer.Send();
                    ViewBag.Message = "A confirmation mail has been sent to you.";
                    // return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }


        public ActionResult VerifyCode(String authID)
        {
            var val = db.PassResets.Where(x => x.AutID == authID);
            var isAvaliable = db.PassResets.Where(x => x.AutID == authID).FirstOrDefault().isAvaliable;
            if (val != null && isAvaliable == true)
            {
                return View("ChangePassword", "Account");
            }
            else return View();
        }


        /* public ActionResult VerifyCode(String authID, String pw)
         {
             var userID = db.PassResets.Where(x => x.AutID == authID).FirstOrDefault().UserID;
             User toBeUpdated = db.Users.Find(userID);
             User updated = toBeUpdated;
             updated.Password = pw;
             db.Entry(toBeUpdated).CurrentValues.SetValues(updated);
             db.SaveChanges();
             var value = db.PassResets.Where(x => x.AutID == authID).FirstOrDefault().isAvaliable;
             value = false;
             return View();
         }*/

        [HttpGet]
        public ActionResult ChangePassword(String authID)
        {


            return View(authID);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel p)
        {
            var isAvaliable = db.PassResets.Where(x => x.AutID == p.passReset.AutID).FirstOrDefault().isAvaliable;
            if (isAvaliable == true)
            {
                var userID = db.PassResets.Where(x => x.AutID == p.passReset.AutID).FirstOrDefault().UserID;
                User toBeUpdated = db.Users.Find(userID);
                User updated = new User();
                updated = toBeUpdated;
                updated.Password = p.user.Password;

                db.Entry(toBeUpdated).CurrentValues.SetValues(updated);
                db.SaveChanges();
                var value = db.PassResets.Where(x => x.AutID == p.passReset.AutID).FirstOrDefault();
                PassReset isAvaliabletoFalse = new PassReset();
                isAvaliabletoFalse = value;
                isAvaliabletoFalse.isAvaliable = false;
                db.Entry(value).CurrentValues.SetValues(isAvaliabletoFalse);
                db.SaveChanges();
                ViewBag.Message = "Your password has been successfully updated.";
                return View();
            }
            else ViewBag.Message = "Authentication code is already used.";
            return View();
        }
    }
}