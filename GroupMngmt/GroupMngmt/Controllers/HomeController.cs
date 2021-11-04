using GroupMngmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupMngmt.Controllers
{
    public class HomeController : Controller
    {
        Model dao = new Model();
        public List<Group> GetGroups()
        {
            return dao.Groups.SqlQuery("select * from Groups ").ToList();
        }

        public int Update()
        {
            int n = 0;
            n = dao.Database.ExecuteSqlCommand("Delete from Group where groupId =1");
            dao.SaveChanges();
            return n;
        }

        public ActionResult Index()
        {
            ViewBag.Groups = GetGroups();
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
    }
}