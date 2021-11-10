using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupMana.Models;

namespace GroupMana.Controllers
{
    public class MemberController : Controller
    {
        Model dao = new Model();
        // GET: Member
        public ActionResult ViewInviation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LeaveGroup(int userId, int groupId)
        {
            if (ModelState.IsValid)
            {
                var mem = dao.Members.Where(s => s.userID == userId && s.groupId == groupId).FirstOrDefault();
                dao.Members.Remove(mem);
                dao.SaveChanges();
                return Redirect("Home/Index");
            }
            return Redirect("Home/Index");
        }
    }
}