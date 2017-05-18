using Blog.DatabaseContext;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult ViewUsers(String UserID)//查看用户个人信息
        {
            Models.UserAccount user = db.UserAccounts.Find(UserID);//找到用户           
            List<Models.Blog> blogs = db.Blogs.Where(m => m.BloggerID == UserID).ToList();//找到用户所有的博文
            ViewBag.blogs = blogs;

            return View(user);
        }

        public ActionResult Lock()
        {
            List<Models.UserAccount> userAccountModels = db.UserAccounts.ToList();//显示数据库中所有的用户
            Models.UserAccount admin = db.UserAccounts.Find("admin");
            userAccountModels.Remove(admin);//除去管理员
            if (userAccountModels.Count != 0)
            {
                return View(userAccountModels);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Lock(string UserID)
        {
            Models.UserAccount user = db.UserAccounts.Find(UserID);//找到用户  
            if (ModelState.IsValid)
            {
                user.status = false;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Content("<script>alert('锁定用户成功！');window.open('" + Url.Content("~/Admin/Lock") + "', '_self')</script>");
            }
            else
                return Content("<script>alert('锁定用户失败！');window.open('" + Url.Content("~/Admin/Lock") + "', '_self')</script>");
        }

        [HttpPost]
        public ActionResult UnLock(string UserID)
        {
            Models.UserAccount user = db.UserAccounts.Find(UserID);//找到用户  
            if (ModelState.IsValid)
            {
                user.status = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Content("<script>alert('解锁用户成功！');window.open('" + Url.Content("~/Admin/Lock") + "', '_self')</script>");
            }
            else
                return Content("<script>alert('解锁用户失败！');window.open('" + Url.Content("~/Admin/Lock") + "', '_self')</script>");
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