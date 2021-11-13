using GroupMana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupMana.Controllers
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
        public ActionResult Index(string username, string password)
        {

            var x = dao.GetUserLogin(username, password);
            if (x != null)
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
        public ActionResult Register(string signupusername, string signuppass, string resignuppass, string mail, string fname)
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
            if (isExist == false)
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
        public ActionResult ForgotPass(string username, string email, string newpassword, string renewpassword)
        {
            if (dao.checkExistUserNameAndEmail(username, email) == false)
            {
                ViewBag.mess = "Username or email does not exist!";
                return View();
            }
            else
            {
                if (!newpassword.Equals(renewpassword))
                {
                    ViewBag.mess = "Password and re-password does not match!";
                    return View();
                }
                else
                {
                    dao.ResetPass(username, newpassword);
                    ViewBag.mess = "Reset password successfully!";
                    return View();
                }
            }
        }
        public ActionResult EditProfile()
        {
            //  int userId = (int)Session["idUser"];
            int userId = 1;
            ViewBag.User  = dao.GetUserByuserid(userId);
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(string fullname, string bio, DateTime dob, string email, int gender)
        {

            //int userId = (int)Session["idUser"];
            int userId = 1;

            var x = model.Users.SingleOrDefault(b => b.userID ==userId);
            if (fullname!=null)
            {
                x.fullname = fullname;
            }
            if (bio!=null)
            {
                x.bio = bio;
            }
            if (email!= null)
            {
                x.email = email;
            }
         


            x.dob = dob;
            x.gender = gender;
            model.SaveChanges();

            ViewBag.User = model.Members.Find(userId);
            return View();
        }
    }
}