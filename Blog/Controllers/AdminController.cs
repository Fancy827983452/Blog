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
        public ActionResult DeleteBlogs(int BlogID, FormCollection form)
        {
            //var SelectedValue = from x in form.AllKeys
            //                    where form[x] != "false"
            //                    select x;//找到你在视图中选定的要删除的数据
            //foreach (var id in SelectedValue)
            //{
            //    if (id != "selectAll")
            //    {
            //        int number = int.Parse(id);
            //        var deleteData = db.Blogs.First(m => m.employeeNumber == number);//找到要删除的数据
            //        db.Blogs.DeleteObject(deleteData);

            //    }
            //    continue;
            //}
            //db.SaveChanges();


            Models.Blog blog = db.Blogs.Find(BlogID);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return Content("<script>alert('博文删除成功！');window.open('" + Url.Content("~/User/Index") + "', '_self')</script>");

        }
        public ActionResult RecommendBlogs()
        {
            return View();
        }
        
    }
}