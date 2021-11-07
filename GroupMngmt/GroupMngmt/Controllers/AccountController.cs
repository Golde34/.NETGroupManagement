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
        public ActionResult Register()
        {
            return View();
        }
    }
}