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
        
        public ActionResult Index()
        {
           
                return View();
           

        }

        [HttpPost]
        public ActionResult Index(string username, string password) {

            var x = dao.GetUserLogin(username, password);
                if (x!=null)
                {

                Session["idUser"] = x.userID;
                //  ViewBag.error = "Sucessful";

                return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }
            
            return View();

        }



        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        Model model = new Model();

        //[HttpPost]
        //public ActionResult Login(string username, string password)
        //{
           

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