using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupMana.Models;

namespace GroupMana.Controllers
{
    public class GroupController : Controller
    {
        Model dao = new Model();
        DAO db = new DAO();
        // GET: Member
        public ActionResult AddGroup()
        {
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            return View(dao.Groups.ToList());
        }

        [HttpPost]
        public ActionResult AddGroup(string groupname, string description, string purpose, string state)
        {
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            Group group = new Group();
            int privateOrPublic;
            Boolean isExist;
            if (db.CheckGroupName(groupname) == true)
            {
                ViewBag.Message = "Group name duplicate";
                isExist = true;
            } 
            else
            {
                isExist = false;
            }
            if (isExist == true)
            {
                return View();
            }
            else
            {
                group.groupName = groupname;
            }
            group.description = description;
            if (state.Equals("Private"))
            {
                privateOrPublic = 0;
            }
            else
            {
                privateOrPublic = 1;
            }
            group.state = privateOrPublic;
            group.purpose = purpose;
            group.status = true;
            dao.Groups.Add(group);
            dao.SaveChanges();
            return RedirectToAction("AddFirstMember", "Group", group);
        }

        public ActionResult AddFirstMember(Group g)
        {
            int id = (int)Session["idUser"];
            User x = dao.Users.SingleOrDefault(b => b.userID == id);
            Member m = new Member { userID = x.userID, groupId = g.groupId, roleId = 1, state = 1, status = true };
            dao.Members.Add(m);
            dao.SaveChanges();
            return RedirectToAction("ViewGroupsOfUser", "Member");
        }

        public ActionResult FindGroup()
        {

            return View(dao.Groups.ToList());
        }

        [HttpPost]
        public ActionResult FindGroup(string searchname)
        {
            var links = (from l in dao.Groups // lấy toàn bộ liên kết
                         select l).ToList();

            if (!String.IsNullOrEmpty(searchname)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.groupName.Contains(searchname)).ToList(); //lọc theo chuỗi tìm kiếm
            }

            return View(links);
        }
        [HttpGet]
        public ActionResult ModifyGroup(int groupId)
        {
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            Group g = db.GetGroupsOfId(groupId);
            ViewBag.g = g;
            return View();
        }

        [HttpPost]
        public ActionResult ModifyGroup(int grId,string groupname, string description, string purpose, string state)
        {
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            var group = dao.Groups.FirstOrDefault(b => b.groupId == grId);
            int privateOrPublic;
            if (db.CheckGroupName(groupname) == true)
            {
                ViewBag.Message = "Group name duplicate";
                ViewBag.g = group;
                return View();
            }
            else
            {
                group.groupName = groupname;
            }
            group.description = description;
            if (state.Equals("Private"))
            {
                privateOrPublic = 0;
            }
            else
            {
                privateOrPublic = 1;
            }
            group.state = privateOrPublic;
            group.purpose = purpose;
            group.status = true;
            dao.SaveChanges();
            return RedirectToAction("ViewGroupsOfUser", "Member", group);
        }

        public ActionResult ViewMember()
        {

            int id = (int)Session["idUser"];
            User x = dao.Users.SingleOrDefault(b => b.userID == id);
            ViewBag.user = x;
            /*var members = dao.Menbers.Where(s => s.groupId == groupId).ToList();
            ViewBag.member = members;*/
            int groupId = (int)Session["groupId"];
            var members = dao.Members.Where(s => s.groupId == groupId && s.status==true && s.state==1).ToList();
            ViewBag.members = members;
            ViewBag.usermember = dao.Members.Where(s => s.groupId == groupId && s.userID == id).FirstOrDefault();
            return View(members);
        }
        public ActionResult InviteMember()
        {
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            var query = dao.Roles.Select(m => new { m.roleId, m.roleName });
            ViewBag.Roles = new SelectList(query.AsEnumerable(), "roleId", "roleName");
            return View();
        }
        [HttpPost]
        public ActionResult InviteMember(string username, int role)
        {
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            int groupId = (int)Session["groupId"];
            bool isExist = false;
            User user = dao.Users.SqlQuery($"select * from Users where username = '{username}'").FirstOrDefault();
            if (user == null)
            {
                ViewBag.message = "User not exist";
                return View();
            }
            var groups = dao.Members.Where(s => s.groupId == groupId && s.userID==user.userID && s.status==true).FirstOrDefault();
            if (groups != null)
            {
                isExist = true;
            }
            if (isExist)
            {
                ViewBag.message = "Member already in group";
                return View();
            }
            Member member = new Member { groupId = groupId, roleId = role, userID = user.userID, status = true, state = 0 };
            dao.Members.Add(member);
            dao.SaveChanges();
            ViewBag.mess = "Invite has been sent";
            return View();
        }
        public ActionResult RemoveMember(int member)
        {
            int group = (int)Session["groupId"];
                var mem = dao.Members.Where(s => s.userID == member && s.groupId == group).First();
                mem.status = false;
                dao.Entry(mem).State = EntityState.Modified;
                dao.SaveChanges();
                return RedirectToAction("ViewMember");
        }
        public ActionResult UpdateMember(string member)
        {    int memberid = int.Parse(member);
            int userId = (int)Session["idUser"];
            User x = dao.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            ViewBag.Member = dao.Members.SingleOrDefault(b => b.userID==memberid);
         
            return View();
        }

        public ActionResult UpdateMemberRole(string role,string userid)
        {
            int memberid = int.Parse(userid);

           var x= dao.Members.SingleOrDefault(b => b.userID == memberid);
            x.roleId = int.Parse(role);
            dao.SaveChanges();
            return RedirectToAction("ViewMember");
        }
    }
}