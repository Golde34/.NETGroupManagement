using GroupMana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupMana.Controllers
{
    public class HomeController : Controller
    {
        Model model = new Model();
        public List<Group> GetGroups()
        {
            return model.Groups.SqlQuery("select * from Groups ").ToList();
        }

        public int Update()
        {
            int n = 0;
            n = model.Database.ExecuteSqlCommand("Delete from Group where groupId =1");
            model.SaveChanges();
            return n;
        }
        public ActionResult Welcome()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Index()
        {
            /*ViewBag.Groups = GetGroups();*/
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var data = model.Users.Where(s => s.email.Equals(email) && s.password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    Session["idUser"] = data.FirstOrDefault().userID;
                    return RedirectToAction("Welcome");
                }
                else
                {
                    ViewBag.message = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var check = model.Users.FirstOrDefault(s => s.email == user.email);
                if (check == null)
                {
                    model.Users.Add(user);
                    model.Configuration.ValidateOnSaveEnabled = false;
                    model.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Email already exists";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Welcome");
        }
    }
}