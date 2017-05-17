using Blog.DatabaseContext;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{

    public class AdminController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewBlogs()
        {
            List<Models.Blog> blogModels = db.Blogs.ToList();//显示数据库中所有的博文
            if (blogModels.Count != 0)
            {
                return View(blogModels);
            }
            else
            {
                return View();
            }
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