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

            //int userId = (int)Session["idUser"];
            int userId = 1;
            ViewBag.Member = dao.GetMemberByuserid(userId);
            var x = dao.GetIssueOfProject(1);
            return View(x);
        }

        

        public ActionResult Delete(string id)
        {
            //int userId = (int)Session["idUser"];
            int isssueid = int.Parse(id);
          
        
            var issue = model.Issues.SingleOrDefault(b => b.issueId == isssueid);
            issue.status = false;
            model.SaveChanges();

            return RedirectToAction("Index", "Issue");
        }

      

        public ActionResult Restore(string id)
        {
            //int userId = (int)Session["idUser"];
            int isssueid = int.Parse(id);

            var issue = model.Issues.SingleOrDefault(b => b.issueId == isssueid);
            issue.status = true;
            model.SaveChanges();

            return RedirectToAction("Index", "Issue");
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