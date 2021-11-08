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
        // GET: Member
        public ActionResult ViewInviation()
        {
            return View();
        }
    }
}