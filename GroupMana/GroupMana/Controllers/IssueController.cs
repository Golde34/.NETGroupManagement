using GroupMana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupMana.Controllers
{
    public class IssueController : Controller
    {
        Model model = new Model();
        DAO dao = new DAO();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditIssue()
        {
            return View();
        }

        [HttpPost]

        public ActionResult EditIssue(int issueid, string title, DateTime duedate, DateTime startdate, string description, string content, int state)
        {
            dao.EditIssue(issueid, title, duedate, startdate, description, content, state);
            ViewBag.mess = "Update succesfully!";
            return View();
        }

    }
}