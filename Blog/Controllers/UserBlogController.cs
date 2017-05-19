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
        [HttpGet]
        public ActionResult seeblogdetails(int BlogID)//查看博文详情
        {
            Models.Blog blog = db.Blogs.Find(BlogID);//找到博文
            Models.UserAccount user = db.UserAccounts.Find(blog.BloggerID);//找到博主的信息存好   

            List<Models.Comment> comments = db.Comments.Where(m => m.BlogID==BlogID).ToList();//吧博文的评论保存好

            //赞数和是否已经点赞过
            String UserID = Session["UserID"].ToString();           
            List<Models.Like> likes = db.Likes.Where(n => n.BlogID == BlogID).Where(m => m.UserID == UserID).ToList();//判断用户是否点赞过
            double likescount = db.Likes.Where(m => m.BlogID == BlogID).Count();//所有的赞数
            if (likes.Count() == 0)//没赞过设置buttom的颜色
            {
                ViewData["background"] = "white";
            }
            else
            {//用户已经赞过 设置颜色
                ViewData["background"] = "pink";
            }
            //判断是否已经关注此人    
            String BloggerID = user.UserID;//博主ID
            List<Models.Focus> focuses = db.Focuses.Where(n => n.DoFocus == UserID).Where(m => m.Focused== BloggerID).ToList(); //判断是否关注此人
            if (focuses.Count() == 0)//没有关注
            {
                ViewData["guanzhu"] = "关注";
            }
            else//关注了
            {
                ViewData["guanzhu"] = "取消关注";
            }
            ViewData["likescount"] = likescount;
            ViewData["BloggerImage"] = user.UserImage;
            ViewData["BloggerID"] = user.UserID;
            ViewData["userName"] = user.UserName;
            ViewBag.comments = comments;
            return View(blog);
        }   


       [HttpGet]
        public ActionResult seeuserdetails(String BloggerID)//查看博主个人信息
        {
            Models.UserAccount user = db.UserAccounts.Find(BloggerID);//找到博主            
            List<Models.Blog> blogs = db.Blogs.Where(m => m.BloggerID == BloggerID).ToList();//找到博主所有的博文
            List<Models.Focus> fans = db.Focuses.Where(m=>m.Focused== BloggerID).ToList();//粉丝数
            List<Models.Focus> focus = db.Focuses.Where(m => m.DoFocus == BloggerID).ToList();//关注的人数
            int fancount = fans.Count();
            int focuscount = focus.Count();
            //判断是否已经关注此人    
            String UserID = Session["UserID"].ToString();//当前用户
            List<Models.Focus> focuses = db.Focuses.Where(n => n.DoFocus == UserID).Where(m => m.Focused == BloggerID).ToList(); //判断是否关注此人
            if (focuses.Count() == 0)//没有关注
            {
                ViewData["guanzhu"] = "关注";
            }
            else//关注了
            {
                ViewData["guanzhu"] = "取消关注";
            }
         
            ViewBag.blogs = blogs;

            ViewData["UserImage"] = user.UserImage;
            ViewData["UserName"] = user.UserName;
            ViewData["fancount"] = fancount;
            ViewData["focuscount"] = focuscount;
            return View(user);
        }
        [HttpGet]
        public ActionResult comment(int BlogID,String ReplyID)//评论
        {
            ViewData["BlogID"] = BlogID;
            ViewData["ReplyID"] = ReplyID;
            return View();
        }
        [HttpPost]
        public ActionResult comment(Models.Comment comment,String BlogID,String ReplyID,String commentcontent)//参数一个是blogID，一个是回复的人
        {
            int BlogID1 = Convert.ToInt32(BlogID);
            try
            {
                comment.BlogID = BlogID1;
                comment.UserID = Session["UserID"].ToString();//发表评论的ID,也就是当前用户
                comment.ReplyID = ReplyID;//被评论的用户ID
                comment.CommentContent= commentcontent;
                comment.CommentTime = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
               // return Content("<script>alert('评论成功！');window.location.href=document.referrer;</script>");
                return Content("<script>alert('评论成功！');window.open('" + Url.Content("~/UserBlog/seeblogdetails?BlogID=" + BlogID1 + "") + "', '_self')</script>");
            }
            catch (Exception e)
            {
               // return Content("<script>alert('评论成功！');window.location.href=document.referrer;</script>");
                return Content("<script>alert('好像有点问题！');window.open('" + Url.Content("~/UserBlog/comment?BlogID=" + BlogID1 + "") + "', '_self')</script>");
            }
     
        }
        [HttpPost]
        public ActionResult like(Models.Like like,String LikeBlogID)//点赞博文的ID
        {
            int BlogID = Convert.ToInt32(LikeBlogID);
            String UserID = Session["UserID"].ToString();
            List<Models.Like> likes= db.Likes.Where(n=>n.BlogID==BlogID).Where(m => m.UserID == UserID).ToList();//判断用户是否点赞过
            if (likes.Count() == 0)//没赞过
            {
                like.BlogID = BlogID;
                like.UserID = Session["UserID"].ToString();
                db.Likes.Add(like);
                db.SaveChanges();
                return Content("<script language=javascript>self.location=document.referrer;</script>");
                //return Content("<script>window.open('" + Url.Content("~/UserBlog/seeblogdetails?BlogID=" + BlogID + "") + "', '_self')</script>");
            }
            else {//用户已经赞过需要取消赞
                foreach (Models.Like h in likes)
                {
                    db.Likes.Remove(h);
                }
                db.SaveChanges();
                return Content("<script language=javascript>self.location=document.referrer;</script>");
                // return Content("<script>window.open('" + Url.Content("~/UserBlog/seeblogdetails?BlogID=" + BlogID + "") + "', '_self')</script>");
            }
       
            }
        [HttpPost]
        public ActionResult focus(Models.Focus focus, String Focused,String BlogID)//处理关注的代码
        {
            int i = 0;//用来判断用户是否关注此人
            String MyUserID = Session["UserID"].ToString();
            int BlogID1 = Convert.ToInt32(BlogID);//用来返回页面
            List<Models.Focus> focuses = db.Focuses.Where(n => n.DoFocus == MyUserID).Where(m=>m.Focused==Focused).ToList(); //判断是否关注此人
       
            if (focuses.Count()==0)//没有关注此人，做关注
            {
                focus.Focused = Focused;
                focus.DoFocus = MyUserID;
                db.Focuses.Add(focus);
                db.SaveChanges();

                // Response.Redirect(Request.Url.ToString());
                return Content("<script language=javascript>self.location=document.referrer;</script>");
        }

        public ActionResult DeleteBlog(int BlogID)
        {
            Models.Blog blog = db.Blogs.Find(BlogID);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return Content("<script>alert('博文删除成功！');window.open('" + Url.Content("~/Admin/ViewBlogs") + "', '_self')</script>");
        }

        public ActionResult RecommandBlog(int BlogID)
        {

            return View();
                foreach (Models.Focus f in focuses)
                {                   
                    db.Focuses.Remove(f);                
        }
                db.SaveChanges();
                return Content("<script language=javascript>self.location=document.referrer;</script>");

            }
          

        }
      

        [HttpGet]
        public ActionResult WriteMessage(String Receiver) 
        {
            ViewData["Receiver"] = Receiver;
            return View();
        }
        [HttpPost]
        public ActionResult WriteMessage(String Receiver,String MessageContent,Models.Message message)//写留言
        {
            try
            {
                message.ReceiverID = Receiver;
                message.SenderID = Session["UserID"].ToString();
                message.MessageContent = MessageContent;
                message.LeaveTime = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
                return Content("<script>alert('留言成功！');window.open('" + Url.Content("~/UserBlog/seeuserdetails?BloggerID=" + Receiver + "") + "', '_self')</script>");
            }
            catch (Exception e)
            {
                return Content("<script>alert('留言失败！');window.open('" + Url.Content("~/UserBlog/seeuserdetails?BloggerID=" + Receiver + "") + "', '_self')</script>");
            }

             
        }
        public ActionResult SendPrivateMessage()
        {
            return View();
        }
    }
}