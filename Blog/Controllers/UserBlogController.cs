using Blog.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class UserBlogController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        // GET: UserBlog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult seeblogdetails(int BlogID)//查看博文详情
        {
            Models.Blog blog = db.Blogs.Find(BlogID);//找到博文
            Models.UserAccount user = db.UserAccounts.Find(blog.BloggerID);//找到博主的信息存好             
            ViewData["BloggerImage"] = user.UserImage;
            ViewData["BloggerID"] = user.UserID;

            return View(blog);
        }
        [HttpGet]
        public ActionResult seeuserdetails(String BloggerID)//查看博主个人信息
        {
            Models.UserAccount user = db.UserAccounts.Find(BloggerID);//找到博主            
            List<Models.Blog> blogs = db.Blogs.Where(m => m.BloggerID == BloggerID).ToList();//找到博主所有的博文


            ViewBag.blogs = blogs;

            return View(user);
        }
    }
}