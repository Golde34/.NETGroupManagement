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
            dao.Projects.Add(project);
            dao.SaveChanges();
            return Redirect("Group");
        }
    }
}