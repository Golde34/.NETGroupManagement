using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupMana.Models;
namespace GroupMana.Controllers
{
    public class AdminController : Controller
    {
        DAO dao = new DAO();
        Model model = new Model();
        // GET: Admin
        public ActionResult UserManagement()
        {
            int userId = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            var users = model.Users.ToList();
            return View(users);
        }
        public ActionResult AddUser()
        {
            int userId = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(string username,string password,string email,string fullname,string gender,string admin)
        {
            int userId = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = x;
            int gen = Convert.ToInt32(gender);
            bool isAdmin = (admin.Equals("1")) ? true : false;
            bool isExist;
            if (dao.checkExistUsername(username) == true)
            {
                ViewBag.mess = "Duplicate username!";
                isExist = true;
            }
            else if (dao.checkExistMail(email) == true)
            {
                ViewBag.mess = "Duplicate mail!";
                isExist = true;
            }
            else
            {
                isExist = false;
            }

            //
            if (isExist == false)
            {
                ViewBag.mess1 = "Add Succesfully";
                User user = new User { username = username, password = password, email = email, fullname = fullname, gender = gen, isAdmin = isAdmin, status = true };
                model.Users.Add(user);
                model.SaveChanges();
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult EditUser(string id)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user1 = x;
            int userId = Convert.ToInt32(id);
            User user = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            ViewBag.user = user;
            return View();
        }
        [HttpPost]
        public ActionResult EditUser(string userid, string username, string password, string email, string fullname, string gender, string admin)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user1 = x;
            int userId = Convert.ToInt32(userid);
            User user = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            int gen = Convert.ToInt32(gender);
            bool isAdmin = (admin.Equals("1")) ? true : false;
            bool isExist=false;
            if (dao.checkExistUsername(username) == true && !username.Equals(user.username))
            {
                ViewBag.mess = "Duplicate username!";
                isExist = true;
            }
            else if (dao.checkExistMail(email) == true && !email.Equals(user.email))
            {
                ViewBag.mess = "Duplicate mail!";
                isExist = true;
            }
            else
            {
                isExist = false;
            }
            if (isExist == false)
            {
                ViewBag.mess1 = "Edit Succesfully";
                user.username = username;
                user.isAdmin = isAdmin;
                user.gender = gen;
                user.email = email;
                user.fullname = fullname;
                user.password = password;
                model.Entry(user).State = EntityState.Modified;
                model.SaveChanges();
                ViewBag.user = user;
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult DeactiveUser(string id)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            int userId = Convert.ToInt32(id);
            User user = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            user.status = false;
            model.Entry(user).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("UserManagement");
        }
        public ActionResult ActiveUser(string id)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            int userId = Convert.ToInt32(id);
            User user = model.Users.Where(s => s.userID == userId).FirstOrDefault();
            user.status = true;
            model.Entry(user).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("UserManagement");
        }
        public ActionResult RoleManagement()
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            var roles = model.Roles.ToList();
            return View(roles);
        }
        public ActionResult AddRole()
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(string rolename)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            Role role = new Role { roleName = rolename ,status=true};
            bool isExist = false;
            if (model.Roles.Where(s => s.roleName.Equals(rolename)).FirstOrDefault() != null)
            {
                isExist = true;
            }
            if (isExist == false)
            {
                ViewBag.mess1 = "Add successfully";
                model.Roles.Add(role);
                model.SaveChanges();
                return View();
            }
            else
            {
                ViewBag.mess = "Exist Role";
                return View();
            }
        }
        public ActionResult EditRole(string id)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            int roleId = Convert.ToInt32(id);
            Role role = model.Roles.Where(s => s.roleId == roleId).FirstOrDefault();
            ViewBag.role = role;
            return View();
        }
        [HttpPost]
        public ActionResult EditRole(string rolename, string id)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            int roleId = Convert.ToInt32(id);
            Role role = model.Roles.Where(s => s.roleId == roleId).FirstOrDefault();
            role.roleName = rolename;
            model.Entry(role).State = EntityState.Modified;
            model.SaveChanges();
            ViewBag.role = role;
            return View();
            /*bool isExist = false;
            if (model.Roles.Where(s => s.roleName.Equals(rolename)).FirstOrDefault() != null && !role.roleName.Equals(rolename))
            {
                isExist = true;
            }
            if (isExist == true)
            {
                ViewBag.mess = "Edit successfully";
                role.roleName = rolename;
                model.Entry(role).State = EntityState.Modified;
                model.SaveChanges();
                return View();
            }
            else
            {
                ViewBag.mess = "Exist Role";
                return View();
            }*/
        }
        public ActionResult DeactiveRole(string id)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            int roleId = Convert.ToInt32(id);
            Role role = model.Roles.Where(s => s.roleId == roleId).FirstOrDefault();
            role.status = false;
            model.Entry(role).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("RoleManagement");
        }
        public ActionResult ActiveRole(string id)
        {
            int uid = (int)Session["idUser"];
            User x = model.Users.Where(s => s.userID == uid).FirstOrDefault();
            ViewBag.user = x;
            int roleId = Convert.ToInt32(id);
            Role role = model.Roles.Where(s => s.roleId == roleId).FirstOrDefault();
            role.status = true;
            model.Entry(role).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("RoleManagement");
        }
    }
}