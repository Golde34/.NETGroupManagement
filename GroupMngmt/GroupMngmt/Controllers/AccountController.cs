using GroupMngmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupMngmt.Controllers
{
    public class AccountController : Controller
    {
        DAO dao = new DAO();
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string pass)
        {
            User x = dao.GetUserLogin(username, pass);
            if (x == null)
            {
                ViewBag.Mess = "Invalid email or password";
                return View();
            }
            Session["user"] = x;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(string username, string pass, string mail, string fullname)
        {
            return View();
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(string email,string password)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var data = dao.Users.Where(s => s.email.Equals(email) && s.password.Equals(password)).ToList();
        //        if (data.Count() > 0)
        //        {
        //            Session["idUser"] = data.FirstOrDefault().userID;
        //            return RedirectToAction("Welcome");
        //        }
        //        else
        //        {
        //            ViewBag.message = "Login failed";
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    return View();
        //}

        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check = dao.Users.FirstOrDefault(s => s.email == user.email);
        //        if (check == null)
        //        {
        //            dao.Users.Add(user);
        //            dao.Configuration.ValidateOnSaveEnabled = false;
        //            dao.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.message = "Email already exists";
        //            return View();
        //        }
        //    }
        //    return View();
        //}
        //public ActionResult Logout()
        //{
        //    Session.Clear();
        //    return RedirectToAction("Welcome");
        //}
    }
}