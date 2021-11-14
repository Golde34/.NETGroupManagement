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

            ViewBag.Messages = "You can not leave this group because you are manager";
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            List<Member> groups = dao.Members.Where(s => s.userID == userId && s.status==true && s.state==1).ToList();
            ViewBag.Invitations = groups;
            return View(groups);
        }
        public ActionResult CreateGroupId(string groupId)
        {
            int group = int.Parse(groupId);
            Session["groupId"] = group;
            return RedirectToAction("ViewProjectOfUser");
        }
        public ActionResult ViewProjectOfUser()
        {
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            int group = (int)Session["groupId"];
            List<Project> groups = dao.Projects.Where( s => s.groupId==group && s.status==true).ToList();
            ViewBag.GroupName = dao.Groups.SingleOrDefault(b => b.groupId == group).groupName;
            ViewBag.Member = dao.Members.Where(s => s.userID == userId && s.groupId == group).FirstOrDefault();
            return View(groups);
        }
        public ActionResult ViewInviation()
        {
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            var invitation = dao.Members.Where(s => s.userID == userId && s.state == 0 && s.status==true).ToList();
            ViewBag.Invitations = invitation;
            return View(invitation);
        }
        public ActionResult AcceptInvitation(string group)
        {
            int userId = (int)Session["idUser"];
            int groupId = Convert.ToInt32(group);
            var invitation = dao.Members.Where(s => s.userID == userId && s.groupId == groupId).FirstOrDefault();
            invitation.state = 1;
            dao.Entry(invitation).State = EntityState.Modified;
            dao.SaveChanges();
            return RedirectToAction("ViewInviation");
        }
        public ActionResult RefuseInvitation(string group)
        {
            int userId = (int)Session["idUser"];
            int groupId = Convert.ToInt32(group);
            var invitation = dao.Members.Where(s => s.userID == userId && s.groupId == groupId).FirstOrDefault();
            invitation.status = false;
            dao.Entry(invitation).State = EntityState.Modified;
            dao.SaveChanges();
            return RedirectToAction("ViewInviation");
        }
        public ActionResult LeaveGroup(int groupId)
        {
            int userId = (int)Session["idUser"];
            Member mem = dao.Members.Where(s => s.userID == userId && s.groupId == groupId).FirstOrDefault();
          
            mem.status = false;
            dao.Entry(mem).State = EntityState.Modified;
            dao.SaveChanges();
            return RedirectToAction("ViewGroupsOfUser");
        }

    }
}