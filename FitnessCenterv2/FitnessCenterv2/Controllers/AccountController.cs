﻿using FitnessCenterv2.Models;
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

            return RedirectToAction("Index","Home");

        }

        /// <summary>
        /// this method displays a register page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {

            return View(new Customer
            {
                Gender = true
            });
        }
        /// <summary>
        /// this method registers the current user as customer.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="cu"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(User c, Customer cu)
        {
            if (Session["UserID"] != null) return RedirectToAction("Index");
            User search = db.Users.Where(x => x.EMail == c.EMail).FirstOrDefault();
            if (ModelState.IsValid && search == null)
            {

                c.Role = "Customer";
                db.Users.Add(c);

                Customer cust = new Customer();

                cust.EMail = c.EMail;
                cust.Password = c.Password;
                cust.FirstName = c.FirstName;
                cust.LastName = c.LastName;
                cust.Gender = cu.Gender;
                cust.Address = cu.Address;
                cust.Phone = cu.Phone;
                cust.CreditCardNumber = cu.CreditCardNumber;
                cust.BirthDate = cu.BirthDate;
                cust.RegistrationDate = DateTime.Now;
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                if(cust.CreditCardNumber != null)
                dt = dt.AddDays(Convert.ToDouble(cu.MemberShipDays));
                cust.EndDateMemberShip = dt;
                cust.MemberShipDays = cu.MemberShipDays;
                db.Customers.Add(cust);
                db.SaveChanges();

                ModelState.Clear();
                TempData["Success"] = cust.EMail + " is registered successfully.";
            }
            else if (search!=null)
            TempData["Error"] = c.EMail + " is already registered to the system.";
            return View();
        }
        /// <summary>
        /// this method shows login page with necessary fields.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if (Session["UserID"] != null) return RedirectToAction("Index");
            return View();
        }
        /// <summary>
        /// this method check whether a userID and password is wrong and logs in.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(User c)
        {
 
            var user = db.Users.Where(x => x.EMail == c.EMail && x.Password == c.Password).FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("", "Email or password wrong.");
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(c.EMail, false);
                Session["UserID"] = user.UserID.ToString();
                Session["UserName"] = user.FirstName + " "+  user.LastName;
                Session["Role"] = user.Role.ToString();
                if (user.Role == "Manager")
                {
                    return RedirectToAction("Index", "Manager");
                }
                if (user.Role == "Staff")
                {
                    return RedirectToAction("Index", "Staff");
                }
                if (user.Role == "Customer" || user.Role == "Expired")
                {
                    Customer cust = db.Customers.Where(x=>x.EMail == user.EMail).FirstOrDefault();
                    if (cust!=null && Convert.ToDateTime(cust.EndDateMemberShip) < DateTime.Now)
                    {
                        TempData["Error"] = "Your membership expired at " + cust.EndDateMemberShip + " You must extend your membership below to continue.";
                        User u2 = user;
                        u2.Role = "Expired";
                        db.Entry(user).CurrentValues.SetValues(u2);
                        db.SaveChanges();
                        return RedirectToAction("refreshMemberShip", "Customer");
                    }
                    return RedirectToAction("Index", "Customer");
                }
                if (user.Role == "Trainer")
                {
                    return RedirectToAction("Index", "Trainer");
                }

            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
                return View();
            else return RedirectToAction("Login");
        }

        /// <summary>
        /// this method logs out a user.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// this method displays a forgot password page.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ForgotPassword(String s)
        {
            if (Session["UserID"] != null) return RedirectToAction("Index");
            return View(s);
        }

        /// <summary>
        /// this method sends a verification code to the email adress given.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword(User s)
        {
            var u = db.Users.Where(x => x.EMail == s.EMail).FirstOrDefault();
            if (u == null)
            {
                ModelState.AddModelError("", "Such user cannot be found.");
                return View();
            }
            else
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
            return View();
        }

        /// <summary>
        /// this method displays a page where user can enter the verification code sent to them.
        /// </summary>
        /// <param name="authID"></param>
        /// <returns></returns>
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
            var isAvaliable = db.PassResets.First().isAvaliable;
            try
            {
            isAvaliable = db.PassResets.Where(x => x.AutID == p.passReset.AutID).FirstOrDefault().isAvaliable;
            }
            catch (Exception ex)
            {
                ViewBag.Message = "There is no such authentication code.";
                Console.WriteLine(ex.Message);
                return View();
                
            }

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