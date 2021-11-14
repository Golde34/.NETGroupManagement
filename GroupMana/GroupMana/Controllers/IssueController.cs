﻿

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
            int projectid = (int)Session["projectId"];

            ViewBag.Member = dao.GetMemberByuserid(userId);
            var x = dao.GetIssueOfProject(projectid);
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
            ViewBag.User = dao.GetUserByuserid(userId);
            int id = int.Parse(iid);
            ViewBag.Issue = model.Issues.FirstOrDefault(i => i.issueId == id);
            return View();
        }

        [HttpPost]

        public ActionResult EditIssue(int issueid, string title, DateTime duedate, DateTime startdate, string description, string content)
        {
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

            ViewBag.Issue = model.Issues.Find(issueid);
            ViewBag.mess = "Update succesfully!";
            return View();
        }

    }
}