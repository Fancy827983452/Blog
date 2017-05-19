using Blog.DatabaseContext;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        // GET: User
        [HttpGet]
        public ActionResult Index()//首页
        {
            String BloggerID = Session["UserID"].ToString();
            List<Models.Blog> models = db.Blogs.Where(m => m.BloggerID == BloggerID).ToList();
            if (models.Count != 0)
            {
                return View(models);
            }
            else
            {
                return View();
            }

        }

        public ActionResult Write()//写博文
        {
            string BloggerID = Session["UserID"].ToString();
            Models.UserAccount user = db.UserAccounts.Find(BloggerID);//找到用户
            if (user.status == false)
                return Content("<script>alert('该账户已处于锁定状态，解锁请与管理员联系！');window.open('" + Url.Content("~/User/Index") + "', '_self')</script>");
            else
                return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Write(Models.Blog blog, FormCollection fc)
        {
            for (int i = 1; i < fc.AllKeys.Length; i++)
            {

                string key = fc.GetKey(i);
                string value = fc.Get(key);
                if (value.Trim().Equals(""))
                    return Content("<script>alert('" + key + "不能为空，请重新输入！');window.open('" + Url.Content("~/User/Write") + "', '_self')</script>");
            }

            try
            {
                blog.BlogTitle = fc["title"];
                blog.BlogContent = fc["editor"];
                blog.Classification = fc["classification"];
                blog.BloggerID = Session["UserID"].ToString();
                blog.CreateTime = DateTime.Now;
                blog.ModifyTime = DateTime.Now;
                db.Blogs.Add(blog);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content("<script>alert('输入好像有点问题！');window.open('" + Url.Content("~/User/Write") + "', '_self')</script>");
            }
            return Content("<script>alert('发表成功');window.open('" + Url.Content("~/User/Index") + "', '_self')</script>");
        }

        public ActionResult EditInfo(FormCollection fc)
        {
            if (@Session["PhoneNumber"] != null)
                fc["tel"] = @Session["PhoneNumber"].ToString();
            if (@Session["UserImage"] != null)
                fc["file"] = @Session["UserImage"].ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo([Bind(Include = "UserID,UserName,Password,UserImage,PhoneNumber,Identification")]UserAccount userAccountModel, FormCollection fc, HttpPostedFileBase file)
        {
            if (fc["username"].ToString().Trim().Equals(""))
                return Content("<script>alert('标记为 * 的输入框不能为空，请重新输入！');window.open('" + Url.Content("~/User/EditInfo") + "', '_self')</script>");
            else
            {
                //this.TryUpdateModel<UserAccount>(userAccountModel);
                var name = db.UserAccounts.Where(m => m.UserName == userAccountModel.UserName);
                if (name.Count() > 1)
                    return Content("<script>alert('该用户昵称已存在！');window.open('" + Url.Content("~/User/EditInfo") + "', '_self')</script>");
                else
                {
                    userAccountModel = db.UserAccounts.Find(Session["UserID"]);
                    //string password = userAccountModel.Password;
                    if (ModelState.IsValid)
                    {
                        #region
                        if (file != null)
                        {
                            try
                            {
                                var fileName = Path.Combine(Request.MapPath("~/File/Images"), Path.GetFileName(file.FileName));
                                file.SaveAs(fileName);  //把在电脑里面选择的图片保存在当前程序的File/Images文件目录下                   
                            }
                            catch (Exception e)
                            {
                                return Content("上传异常 ！", "text/plain");
                                //return Content("<script>alert('上传异常 ！');window.open('" + Url.Content("~/User/EditInfo") + "', '_self')</script>");
                            }

                            //db.UserAccounts.Remove(userAccountModel);
                            //db.SaveChanges();
                            //userAccountModel.UserID = Session["UserID"].ToString();
                            userAccountModel.UserName = fc["userName"];
                            //userAccountModel.Password = password;
                            userAccountModel.UserImage = file.FileName;//仅仅保存图片的名字
                            userAccountModel.PhoneNumber = fc["tel"];
                            //userAccountModel.Identification = "user";
                            //db.UserAccounts.Add(userAccountModel);
                            db.Entry(userAccountModel).State = EntityState.Modified;
                            db.SaveChanges();

                            Session["UserName"] = userAccountModel.UserName;
                            Session["PhoneNumber"] = userAccountModel.PhoneNumber;
                            Session["UserImage"] = userAccountModel.UserImage;
                            Session["Identification"] = userAccountModel.Identification;
                            return Content("<script>alert('修改个人信息成功！');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                        }
                        else
                            return Content("<script>alert('没有文件！');window.open('" + Url.Content("~/User/EditInfo") + "', '_self')</script>");
                        #endregion 
                    }
                    else
                    {   //获取所有错误的Key                
                        List<string> Keys = ModelState.Keys.ToList();
                        //获取每一个key对应的ModelStateDictionary                
                        foreach (var key in Keys)
                        {
                            var errors = ModelState[key].Errors.ToList();
                            //将错误描述输出到控制台                    
                            foreach (var error in errors)
                            {
                                Console.WriteLine(error.ErrorMessage);
                            }
                        }
                    }
                }
            }
            return View();
        }
        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(UserAccount userAccountModel, FormCollection fc)
        {
            for (int i = 1; i < fc.AllKeys.Length; i++)
            {
                string key = fc.GetKey(i);
                string value = fc.Get(key);
                if (value.Trim().Equals(""))
                    return Content("<script>alert('标记为 * 的输入框不能为空，请重新输入！');window.open('" + Url.Content("~/User/EditPassword") + "', '_self')</script>");
            }
            if (fc["password_new"].Equals(fc["password_new2"]))//两次输入密码一致
            {
                userAccountModel = db.UserAccounts.Find(Session["UserID"]);
                string password = userAccountModel.Password;
                if (fc["password_old"].Equals(password))//原密码输入正确
                {
                    if (ModelState.IsValid)
                    {
                        //db.UserAccounts.Remove(userAccountModel);
                        //db.SaveChanges();
                        //userAccountModel.UserID = Session["UserID"].ToString();
                        //userAccountModel.UserName = Session["UserName"].ToString();
                        userAccountModel.Password = fc["password_new"];
                        //userAccountModel.UserImage = Session["UserImage"].ToString();
                        //userAccountModel.PhoneNumber = Session["PhoneNumber"].ToString();
                        //userAccountModel.Identification = "user";
                        //db.UserAccounts.Add(userAccountModel);
                        db.Entry(userAccountModel).State = EntityState.Modified;
                        db.SaveChanges();

                        Session["UserID"] = userAccountModel.UserID;
                        Session["UserName"] = userAccountModel.UserName;
                        Session["Identification"] = userAccountModel.Identification;
                        return Content("<script>alert('修改密码成功！');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                    }
                }
                else
                    return Content("<script>alert('原密码输入错误！');window.open('" + Url.Content("~/User/EditPassword") + "', '_self')</script>");

            }
            else
                return Content("<script>alert('两次输入的密码不一致！');window.open('" + Url.Content("~/User/EditPassword") + "', '_self')</script>");

            return View();
        }

        public ActionResult Focus()
        {
            String MyUserID = Session["UserID"].ToString();
            List<Models.Focus> focuses = db.Focuses.Where(m => m.DoFocus == MyUserID).ToList();//所以我关注的人
            return View(focuses);
        }
        public ActionResult AtMe()//回复我的消息
        {
            String MyUserID = Session["UserID"].ToString();
            List<Models.Comment> comments = db.Comments.Where(m => m.ReplyID == MyUserID).OrderByDescending(m => m.CommentTime).ToList();//所以回复我评论的评论
            return View(comments);
        }



        public ActionResult Message(String UserID)//查看留言
        {
            // String MyUserID = Session["UserID"].ToString();
            List<Models.Message> messages = db.Messages.Where(m => m.ReceiverID == UserID).OrderByDescending(m => m.LeaveTime).ToList();
            return View(messages);
        }
        public ActionResult Fans()//查看我的粉丝
        {
            String MyUserID = Session["UserID"].ToString();
            List<Models.Focus> focus = db.Focuses.Where(m => m.Focused == MyUserID).ToList();
            return View(focus);
        }
        [HttpGet]
        public ActionResult blogdetails(int BlogID)//查看博文详情
        {

            Models.Blog blog = db.Blogs.Find(BlogID);//找到博文      
            List<Models.Comment> comments = db.Comments.Where(m => m.BlogID == BlogID).ToList();//吧博文的评论保存好

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

            ViewData["likescount"] = likescount;

            ViewBag.comments = comments;
            return View(blog);


        }
        public ActionResult blogdelete(int BlogID)//博文删除
        {
            Models.Blog blog = db.Blogs.Find(BlogID);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return Content("<script>alert('博文删除成功！');window.open('" + Url.Content("~/User/Index") + "', '_self')</script>");
        }
        [HttpGet]
        public ActionResult blogedit(int BlogID)//博文修改
        {
            string BloggerID = Session["UserID"].ToString();
            Models.UserAccount user = db.UserAccounts.Find(BloggerID);//找到用户
            if (user.status == false)
                return Content("<script>alert('该账户已处于锁定状态，解锁请与管理员联系！');window.open('" + Url.Content("~/User/Index") + "', '_self')</script>");
            else
            {
                Models.Blog blog = db.Blogs.Find(BlogID);
                return View(blog);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult blogedit(FormCollection fc, int BlogID)
        {
            for (int i = 1; i < fc.AllKeys.Length; i++)
            {

                string key = fc.GetKey(i);
                string value = fc.Get(key);
                if (value.Trim().Equals(""))
                    return Content("<script>alert('" + key + "不能为空，请重新输入！');window.open('" + Url.Content("~/UserBlog/seeblogdetails?BlogID=" + BlogID + "") + "', '_self')</script>");
            }
            try
            {
                Models.Blog editblog = db.Blogs.Find(BlogID);
                editblog.BlogTitle = fc["title"];
                editblog.BlogContent = fc["editor"];
                editblog.Classification = fc["classification"];
                editblog.ModifyTime = DateTime.Now;
                db.Entry(editblog).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception e)
            {
                return Content("<script>alert('修改过程好像有点问题！');window.open('" + Url.Content("~/UserBlog/seeblogdetails?BlogID=" + BlogID + "") + "', '_self')</script>");
            }
            return Content("<script>alert('修改成功');window.open('" + Url.Content("~/UserBlog/seeblogdetails?BlogID=" + BlogID + "") + "', '_self')</script>");
        }

    }
}