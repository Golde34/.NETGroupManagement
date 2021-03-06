

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

        public ActionResult Check(string g)
        {

            int userId = (int)Session["idUser"];
            int projectid = int.Parse(g);
            Session["projectId"] = projectid;

            return RedirectToAction("Index", "Issue");
        }
        public ActionResult Index()
        {
            int userId = (int)Session["idUser"];
            User user = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = user;
            int groupId = (int)Session["groupId"];
            int projectid = (int)Session["projectId"];

           var member = model.Members.Where(s => s.groupId == groupId && s.userID == userId && s.status==true).FirstOrDefault();
            ViewBag.Member = member;
            var x = dao.GetIssueOfProject(projectid);
            if (member.roleId!=1)
            {
              x = dao.GetIssueOfProjectTrue(projectid);
            }
            

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

        public ActionResult EditIssue(string iid)
        {
            int userId = (int)Session["idUser"];
            ViewBag.Member = model.Members.FirstOrDefault(i => i.userID == userId);
            int id = int.Parse(iid);
            ViewBag.Issue = model.Issues.FirstOrDefault(i => i.issueId == id);
            return View();
        }

        [HttpPost]

        public ActionResult EditIssue(int issueid, string title, DateTime duedate, DateTime startdate, string description, string content)
        {
            int userId = (int)Session["idUser"];
            User user = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = user;
            var issue = model.Issues.SingleOrDefault(b => b.issueId == issueid);
            if (title != null)
            {
                issue.title = title;
            }
            if (description != null)
            {
                issue.description = description;
            }
            if (content != null)
            {
                issue.content = content;
            }
            issue.dueDate = duedate;
            issue.startDate = startdate;
            model.SaveChanges();
            ViewBag.Member = model.Members.FirstOrDefault(i => i.userID == userId);
            ViewBag.Issue = model.Issues.Find(issueid);
            ViewBag.mess = "Update succesfully!";
            return View();
        }

        public ActionResult AddIssue()
        {
            int userId = (int)Session["idUser"];
            User user = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = user;
            int groupId = (int)Session["groupId"];
            var members = model.Members.Where(s => s.groupId == groupId &&s.state==1&&s.status==true).ToList();
            return View(members);
        }
        [HttpPost]
        public ActionResult AddIssue(string title, DateTime duedate, DateTime startdate, string description, string content,int assignee)
        {
            int userId = (int)Session["idUser"];
            User user = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = user;
            int groupId = (int)Session["groupId"];
            var members = model.Members.Where(s => s.groupId == groupId && s.state == 1 && s.status == true).ToList();
            int creator = (int)Session["idUser"];
            int projectId = (int)Session["projectId"];
            Issue issue = new Issue { assignee = assignee, content = content, creator = creator, description = description, dueDate = duedate, projectId = projectId, startDate = startdate, title = title, state = 1, status = true };
            model.Issues.Add(issue);
            ViewBag.mess = "Add issue success!";
            model.SaveChanges();
            return View(members);
        }

        [HttpGet]
        public ActionResult IssueDetail(String iid)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.SingleOrDefault(b => b.userID == uid);
            ViewBag.user = x;
            int id = int.Parse(iid);
            ViewBag.Issue = model.Issues.FirstOrDefault(i => i.issueId == id);
            return View();
        }

        [HttpPost]
        public ActionResult IssueDetail(int issueid, int state)
        {
            var issue = model.Issues.SingleOrDefault(b => b.issueId == issueid);

            issue.state = state;
            model.SaveChanges();

            ViewBag.Issue = model.Issues.Find(issueid);
            ViewBag.mess = "Update succesfully!";
            return View();
        }
    }
}