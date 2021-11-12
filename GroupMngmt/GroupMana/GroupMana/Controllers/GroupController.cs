using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupMana.Models;

namespace GroupMana.Controllers
{
    public class GroupController : Controller
    {
        Model model = new Model();
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddGroup()
        {
            return View(model.Groups.ToList());
        }
        [HttpPost]
        public ActionResult AddGroup(string groupname, string description, string purpose, string state)
        {
            Group group = new Group();
            int privateOrPublic = 0;
            group.groupName = groupname;
            group.description = description;
            if (state.Equals("Private"))
            {
                privateOrPublic = 0;
            } else
            {
                privateOrPublic = 1;
            }
            group.state = privateOrPublic;
            group.purpose = purpose;
            group.status = true;
            model.Groups.Add(group);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FindGroup()
        {

            return View(model.Groups.ToList());
        }
        [HttpPost]
        public ActionResult FindGroup(string searchname)
        {
            var links = (from l in model.Groups // lấy toàn bộ liên kết
                         select l).ToList();

            if (!String.IsNullOrEmpty(searchname)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.groupName.Contains(searchname)).ToList(); //lọc theo chuỗi tìm kiếm
            }

            return View(links);
        }
        public ActionResult ViewMember()
        {
            /*var members = model.Menbers.Where(s => s.groupId == groupId).ToList();
            ViewBag.member = members;*/
            return View();
        }
        [HttpPost]
        public ActionResult ViewMember(int groupId)
        {
            var members = model.Members.Where(s => s.groupId == groupId).ToList();
            ViewBag.members = members;
            return View();
        }
        public ActionResult InviteMember()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InviteMember(string email, int role, int group)
        {
            if (ModelState.IsValid)
            {
                bool isExist = false;
                User user = (User)model.Users.Where(s => s.email == email);
                var groups = from g in model.Groups
                             join mem in model.Members on g.groupId equals mem.groupId
                             where mem.userID == user.userID
                             select g;
                foreach (Group item in groups)
                {
                    if (item.groupId == item.groupId)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    ViewBag.message = "Member already in group";
                    return View();
                }
                Member member = new Member { groupId = group, roleId = role, userID = user.userID, status = 0 };
                model.Members.Add(member);
                model.SaveChanges();
                return Redirect("Home/Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult RemoveMember(int member, int group)
        {
            if (ModelState.IsValid)
            {
                var mem = model.Members.Where(s => s.userID == member && s.groupId == group).First();
                model.Members.Remove(mem);
                model.SaveChanges();
                return RedirectToAction("ViewMember");
            }
            return RedirectToAction("ViewMember");
        }
    }
}