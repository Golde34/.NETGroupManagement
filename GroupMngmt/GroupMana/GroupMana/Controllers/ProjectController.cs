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
        Model model = new Model();
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
            model.Projects.Add(project);
            model.SaveChanges();
            return Redirect("Group");
        }
    }
}