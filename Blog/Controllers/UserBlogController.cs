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
            ViewData["BlogID"] = BlogID;

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
        public ActionResult WriteMessage()
        {
            return View();
        }
        public ActionResult SendPrivateMessage()
        {
            return View();
        }


        public ActionResult Like()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Like(int BlogID)
        {
            Like LikeModel = new Like();
            string userID = Session["UserID"].ToString();

            //获取当前博文的总赞数
            var likes = db.Likes.Where(m => m.BlogID == BlogID).Where(m => m.status == true);
            int likeAmount = likes.Count();
            if (likeAmount < 0)
                likeAmount = 0;
            Session["likeAmount"] = likeAmount;

            //找到数据库中指定BlogID下的UserID
            var userIDs = db.Likes.Where(m => m.BlogID == BlogID).Where(m => m.UserID == userID);
            if (userIDs.Count() == 1) //用户存在
            {
                if (LikeModel.status == true) //用户已赞
                {
                    try
                    {
                        LikeModel.Time = DateTime.Now;
                        LikeModel.status = false;
                        db.Entry(LikeModel).State = EntityState.Modified;
                        db.SaveChanges();
                        likeAmount--;
                        Session["likeAmount"] = likeAmount;
                        return PartialView("Like");
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                else //用户已赞但取消了赞
                {
                    try
                    {
                        LikeModel.Time = DateTime.Now;
                        LikeModel.status = true;
                        db.Entry(LikeModel).State = EntityState.Modified;
                        db.SaveChanges();
                        likeAmount++;
                        Session["likeAmount"] = likeAmount;
                        return PartialView("Like");
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
            else //用户未点过赞
            {
                try
                {
                    LikeModel.BlogID = BlogID;
                    LikeModel.UserID = userID;
                    LikeModel.Time = DateTime.Now;
                    LikeModel.status = true;
                    db.Likes.Add(LikeModel);
                    db.SaveChanges();
                    likeAmount++;
                    Session["likeAmount"] = likeAmount;
                    return PartialView("Like");
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}