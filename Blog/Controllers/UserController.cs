using Blog.DatabaseContext;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Write()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Write(FormCollection fc, Models.Blog blog)
        {
            for (int i = 1; i < fc.AllKeys.Length; i++)
            {
                string key = fc.GetKey(i);
                string value = fc.Get(key);
                if (value.Trim().Equals(""))
                    return Content("<script>alert(‘输入框不能为空，请重新输入！');window.open('" + Url.Content("~/User/Write") + "', '_self')</script>");
            }
            blog.BlogTitle = fc["title"];
            blog.Classification = fc["classification"];
            blog.BlogContent = "abc";
            blog.BloggerID = Session[""];
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