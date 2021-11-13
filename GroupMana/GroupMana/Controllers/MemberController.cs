using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupMana.Models;
using System.Data.Entity;

namespace GroupMana.Controllers
{
    public class MemberController : Controller
    {
        DAO d = new DAO();
        Model dao = new Model();
        // GET: Member
        public ActionResult ViewGroupsOfUser()
        {
            int userId = (int)Session["idUser"];
            List<Member> groups = dao.Members.Where(s => s.userID == userId && s.status==true).ToList();
            ViewBag.Invitations = groups;
            return View(groups);
        }
        public ActionResult ViewInviation()
        {
            int userId = (int)Session["idUser"];
            var invitation = dao.Members.Where(s => s.userID == userId && s.state == 0).ToList();
            ViewBag.Invitations = invitation;
            return View(invitation);
        }
        [HttpPost]
        public ActionResult HandleInvitation(string action, int groupId)
        {
            int userId = (int)Session["idUser"];
            var invitation = dao.Members.Where(s => s.userID == userId && s.groupId == groupId).FirstOrDefault();
            if (action.Equals("accept"))
            {
                invitation.status = true;
                dao.Entry(invitation).State = EntityState.Modified;
                dao.SaveChanges();
                return RedirectToAction("ViewInviation");
            }
            else if (action.Equals("refuse"))
            {
                invitation.status = false;
                dao.Entry(invitation).State = EntityState.Modified;
                dao.SaveChanges();
                return RedirectToAction("ViewInviation");
            }
            return RedirectToAction("ViewInviation");
        }
        public ActionResult LeaveGroup(int groupId)
        {
            int userId = (int)Session["idUser"];
            Member mem = dao.Members.Where(s => s.userID == userId && s.groupId == groupId).FirstOrDefault();
            if (mem.roleId == 1)
            {
                ViewBag.Messages = "You can not leave this group because you are manager";
                return RedirectToAction("ViewGroupsOfUser");
            }
            mem.status = false;
            dao.Entry(mem).State = EntityState.Modified;
            dao.SaveChanges();
            return RedirectToAction("ViewGroupsOfUser");
        }
    }
}