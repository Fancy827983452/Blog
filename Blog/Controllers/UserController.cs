using Blog.DatabaseContext;
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

        public ActionResult Write()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Write(Blog.Models.Blog BlogModel, FormCollection fc)
        {
            for (int i = 1; i < fc.AllKeys.Length-1; i++)
            {
                string key = fc.GetKey(i);
                string value = fc.Get(key);
                if (value.Trim().Equals(""))
                    return Content("<script>alert('博文标题不能为空，请重新输入！');window.open('" + Url.Content("~/User/Write") + "', '_self')</script>");
            }
            //String word_content = Request.("editor");
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