using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupMana.Models;

namespace GroupMana.Controllers
{
    public class ProjectController : Controller
    {
        Model dao = new Model();
        DAO db = new DAO();
        // GET: Project
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(string projectname, string description)
        {
            Project project = new Project();
            project.projectName = projectname;
            project.description = description;
            project.createdate = DateTime.Now;
            project.groupId = (int)Session["groupId"];
            project.status = true;
            Group group = db.GetGroupsOfId((int)Session["groupId"]);
            dao.Projects.Add(project);
            dao.SaveChanges();
            return RedirectToAction("ViewProjectOfuser", "Member", group);
        }
    }
}