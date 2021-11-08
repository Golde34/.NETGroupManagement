using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupMngmt.Models;

namespace GroupMngmt.Controllers
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
        public ActionResult LeaveGroup(int userId,int groupId)
        {
            if (ModelState.IsValid)
            {
                var mem = dao.Menbers.Where(s => s.userID == userId&&s.groupId==groupId).FirstOrDefault();
                dao.Menbers.Remove(mem);
                dao.SaveChanges();
                return Redirect("Home/Index");
            }
            return Redirect("Home/Index");
        }
    }
}