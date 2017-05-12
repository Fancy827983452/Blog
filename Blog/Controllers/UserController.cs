using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Write()
        {
            return View();
        }
        public ActionResult EditInfo()
        {
            return View();
        }
        public ActionResult EditPassword()
        {
            return View();
        }
        public ActionResult Focus()
        {
            return View();
        }
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult Fans()
        {
            return View();
        }
    }
}