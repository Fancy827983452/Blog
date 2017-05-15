using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewBlogs()
        {
            return View();
        }

        public ActionResult Lock()
        {
            return View();
        }
        public ActionResult UnLock()
        {
            return View();
        }
        public ActionResult DeleteBlogs()
        {
            return View();
        }
        public ActionResult RecommendBlogs()
        {
            return View();
        }
        
    }
}