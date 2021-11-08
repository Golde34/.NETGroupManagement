using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupMngmt.Models;

namespace GroupMngmt.Controllers
{
    public class DAO : Controller
    {
        Model db = new Model();
        // User DAO
        #region
        public User GetUserLogin(string username,string pass)
        {
            try
            {
                User x = db.Users.SqlQuery($"select * from [Users] where BINARY_CHECKSUM(username) = BINARY_CHECKSUM('{username}') and BINARY_CHECKSUM([password]) = BINARY_CHECKSUM('{pass}')").First();
                return x;
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
                return null;
            }
        }
        
        public User GetUserByEmail(string email)
        {
            try
            {
                User x = db.Users.SqlQuery($"SELECT * FROM [User] WHERE BINARY_CHECKSUM(email) = BINARY_CHECKSUM('{email}')").First();
                return x;
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                return null;
            }
        }

        public void RegistUser(string email, string password, string fullname,string username)
        {
            try
            {
                db.Database.ExecuteSqlCommand($"insert into Users(email, password, fullname, username, profileimage, status ) values ('{email}', '{password}', '{fullname}', '{username}', 'link', 1)");
                db.SaveChanges();
            }
            catch(Exception ex){
                ex.StackTrace.ToString();
            }
            
        }

        public Boolean checkExistUsername(string username)
        {
            Boolean isExist = false;
            try
            {
                var x = db.Users.SqlQuery($"select * from [Users] where BINARY_CHECKSUM(username) = BINARY_CHECKSUM('{username}')").First();
                if (x != null)
                {
                    isExist = true;
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }

            return isExist;
        }

        public Boolean checkExistMail(string mail)
        {
            Boolean isExist = false;
            try
            {
                var x = db.Users.SqlQuery($"select * from [Users] where BINARY_CHECKSUM(email) = BINARY_CHECKSUM('{mail}')").First();
                if (x != null)
                {
                    isExist = true;
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }

            return isExist;
        }

        #endregion
        // Group DAO
        #region
        public void AddNewGroup(string name,string description)
        {
            try
            {
                db.Database.ExecuteSqlCommand($"insert into Groups(groupName,description,status) values({name}, {description}, {true})");
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }
        public void AddNewMemberToGroup(int UserId,int groupId)
        {
            try
            {
                db.Menbers.Add(new Models.Menber { userID = UserId, groupId = groupId, status = true });
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }
        public void AddNewProjectToGroup(int groupId,string name,string description)
        {
            try
            {
                db.Projects.Add(new Project { groupId = groupId, description = description, projectName = name, status = true });
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }
        //Cái này sai sửa sau
        public List<Group> GetGroupsOfUser(int userId)
        {
            try
            {
                var groups = from g in db.Groups
                             join mem in db.Menbers on g.groupId equals mem.groupId
                             where mem.userID == userId
                             select g;
                return groups.ToList();
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
                return null;
            }
        }
        public List<Project> GetProjectsOfGroup(int groupId)
        {
            try
            {
                var projects = from prj in db.Projects where prj.groupId == groupId select prj;
                return projects.ToList();
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
                return null;
            }
        }
        #endregion
        //Issue DAO
        #region
        public void AddNewIssueToGroup(int userId,string title,int projectId,string issueName,string description,DateTime dueDate,string content,DateTime start)
        {
            try
            {
                db.Issues.Add(new Issue {
                    description=description,
                    projectId=projectId,
                    content=content,
                    state=1,
                    dueDate=dueDate,
                    startDate=start,
                    title=title,
                    creator=userId
                });
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
            }
        }
        public void AssignIssueToMember(int issueId,int member)
        {
            
        }
        public List<Issue> GetIssueOfProject(int projectId)
        {
            try
            {
                var issues = from issue in db.Issues where issue.projectId == projectId select issue;
                return issues.ToList();
            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
                return null;
            }
        }
        #endregion
        //Role DAO
        #region
        public void AddNewRole(string roleName)
        {
            db.Roles.Add(new Role { roleName = roleName, status = true });
            db.SaveChanges();
        }
        #endregion
    }
}