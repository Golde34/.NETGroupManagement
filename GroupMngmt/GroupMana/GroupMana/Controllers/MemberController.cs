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
        Model model = new Model();
        // GET: Member
        public ActionResult ViewInviation()
        {
            int userId = (int)Session["idUser"];
            var invitation = model.Members.Where(s => s.userID == userId && s.status == 0).ToList();
            ViewBag.Invitations = invitation;
            return View();
        }
        [HttpPost]
        public ActionResult HandleInvitation(string action, int groupId)
        {
            int userId = (int)Session["idUser"];
            var invitation = model.Members.Where(s => s.userID == userId && s.groupId == groupId).FirstOrDefault();
            if (action.Equals("accept"))
            {
                invitation.status = 1;
                model.Entry(invitation).State = EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("ViewInviation");
            }else if (action.Equals("refuse"))
            {
                invitation.status = -1;
                model.Entry(invitation).State = EntityState.Modified;
                model.SaveChanges();
                return RedirectToAction("ViewInviation");
            }
            return RedirectToAction("ViewInviation");
        }
        [HttpPost]
        public ActionResult LeaveGroup(int userId, int groupId)
        {
            if (ModelState.IsValid)
            {
                var mem = model.Members.Where(s => s.userID == userId && s.groupId == groupId).FirstOrDefault();
                model.Members.Remove(mem);
                model.SaveChanges();
                return Redirect("Home/Index");
            }
            return Redirect("Home/Index");
        }
    }
}