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

            List<Models.Focus> fans = db.Focuses.Where(m => m.Focused == UserID).ToList();//粉丝数
            List<Models.Focus> focus = db.Focuses.Where(m => m.DoFocus == UserID).ToList();//关注的人数
            int fancount = fans.Count();
            int focuscount = focus.Count();

            ViewBag.blogs = blogs;
            ViewData["fancount"] = fancount;
            ViewData["focuscount"] = focuscount;
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
        public ActionResult SearchUser(FormCollection fc)
        {
            string UserID = fc["search"];
            List<Models.UserAccount> user = db.UserAccounts.Where(m => m.UserID == UserID).ToList();//找到用户  
            if (user.Count != 0)
            {
                return View(user);
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult SearchBlog(FormCollection fc)
        {
            int BlogID = Convert.ToInt32(fc["search"]);
            List<Models.Blog> blog = db.Blogs.Where(m => m.BlogID == BlogID).ToList();//找到博文 
            if (blog.Count != 0)
            {
                return View(blog);
            }
            else
                return View();
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

        [HttpPost]
        public ActionResult DeleteBlogs(int BlogID)
        {
            Models.Blog blog = db.Blogs.Find(BlogID);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return Content("<script>alert('博文删除成功！');window.open('" + Url.Content("~/Admin/ViewBlogs") + "', '_self')</script>");
        }

        [HttpPost]
        public ActionResult RecommendBlogs(int BlogID)
        {
            Models.Recommend recommendModel = new Recommend();
            Models.Blog blog = db.Blogs.Find(BlogID);
            var recommends = db.Recommends.Where(m => m.BlogID == BlogID);
            if (recommends.Count() == 0)//没有这条记录
            {
                if (ModelState.IsValid)
                {
                    recommendModel.BlogID = BlogID;
                    recommendModel.BlogTitle = blog.BlogTitle;
                    recommendModel.Time = DateTime.Now;
                    db.Recommends.Add(recommendModel);//将记录插入到推荐表中
                    blog.isRecommended = true;//将博文表中的是否推荐改为是
                    db.SaveChanges();
                    return Content("<script>alert('博文推荐成功！');window.open('" + Url.Content("~/Admin/ViewBlogs") + "', '_self')</script>");
                }
            }
            return Content("<script>alert('博文推荐失败！');window.open('" + Url.Content("~/Admin/ViewBlogs") + "', '_self')</script>");
        }

        [HttpPost]
        public ActionResult CancelRecommendBlogs(int BlogID)
        {
            var result = db.Recommends.Where(m => m.BlogID == BlogID);
            int RecommendID = result.First().RecommendID;
            Models.Recommend recommendModel = db.Recommends.Find(RecommendID);
            db.Recommends.Remove(recommendModel);//删除推荐表中的记录
            Models.Blog blog = db.Blogs.Find(BlogID);
            blog.isRecommended = false;//将博文表中的是否推荐改为否
            db.SaveChanges();
            return Content("<script>alert('取消推荐成功！');window.open('" + Url.Content("~/Admin/ViewBlogs") + "', '_self')</script>");

        }

        public ActionResult seeblogdetails(int BlogID)//查看博文详情
        {
            Models.Blog blog = db.Blogs.Find(BlogID);//找到博文
            Models.UserAccount user = db.UserAccounts.Find(blog.BloggerID);//找到博主的信息存好   
            List<Models.Comment> comments = db.Comments.Where(m => m.BlogID == BlogID).ToList();//吧博文的评论保存好
            List<Models.Focus> fans = db.Focuses.Where(m => m.Focused == blog.BloggerID).ToList();//粉丝数
            List<Models.Focus> focus = db.Focuses.Where(m => m.DoFocus == blog.BloggerID).ToList();//关注的人数
            int fancount = fans.Count();
            int focuscount = focus.Count();

            //赞数和是否已经点赞过
            String UserID = Session["UserID"].ToString();
            List<Models.Like> likes = db.Likes.Where(n => n.BlogID == BlogID).Where(m => m.UserID == UserID).ToList();//判断用户是否点赞过
            double likescount = db.Likes.Where(m => m.BlogID == BlogID).Count();//所有的赞数

            ViewData["likescount"] = likescount;
            ViewData["BloggerImage"] = user.UserImage;
            ViewData["BloggerID"] = user.UserID;
            ViewData["userName"] = user.UserName;
            ViewData["userID"] = user.UserID;
            ViewData["fancount"] = fancount;
            ViewData["focuscount"] = focuscount;
            ViewBag.comments = comments;
            return View(blog);
        }

    }
}