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
        Model model = new Model();
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
   

        [HttpPost]
        public ActionResult Register(string signupusername, string signuppass,string resignuppass, string mail, string fname)
        {
                Boolean isExist = false;
                if (dao.checkExistUsername(signupusername) == true)
                {
                ViewBag.messRegis = "Duplicate username!";
                    isExist = true;
                }
                else if (dao.checkExistMail(mail) == true)
                {
                ViewBag.messRegis = "Duplicate mail!";
                    isExist = true;
                }
                else
                {
                    isExist = false;
                }
                
                //
                if(isExist == false)
                {
                    dao.RegistUser(mail, signuppass, fname, signupusername);
                    ViewBag.MessRegis = "Signup successfully";
                    return View();
                }
                else
                {
                    return View();
                }
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(string email, string password)

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
        public ActionResult ForgotPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPass(string username, string email)
        {
            if (ModelState.IsValid)
            {
                var data = model.Users.Where(s => s.username.Equals(username) && s.email.Equals(email)).ToList();
                if (data.Count() > 0)
                {
                    ViewBag.message = "Reset password successfully!";
                    return RedirectToAction("ForgotPass");
                }
                else
                {
                    ViewBag.message = "Invalid username or email!";
                    return RedirectToAction("ForgotPass");
                }
            }
            return View();
        }
    }
}