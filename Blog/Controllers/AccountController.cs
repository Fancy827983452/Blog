using Blog.DatabaseContext;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{

    public class AccountController : Controller
    {

        private BlogDbContext db = new BlogDbContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string userID,string password)
        {
            if ((userID.Trim().Equals("")) || (password.Trim().Equals("")))
                return Content("<script>alert('用户ID和密码不能为空！');window.open('" + Url.Content("~/Account/SignIn") + "', '_self')</script>");
            else
            {
                var users = db.UserAccounts.Where(m => m.UserID == userID);
                if (users.Count() == 1)//根据ID找到该用户
                {
                    foreach (var u in users)
                    {
                        if (u.Password == password)
                        {
                            Session["UserID"] = u.UserID;
                            Session["UserName"] = u.UserName;
                            Session["Identification"] = u.Identification;
                            return Content("<script>alert('登录成功，欢迎回来！');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                        }
                        else
                        {
                            return Content("<script>alert('密码错误！');window.open('" + Url.Content("~/Account/SignIn") + "', '_self')</script>");
                        }
                    }
                }
                else
                {
                    return Content("<script>alert('该用户名不存在！');window.open('" + Url.Content("~/Account/SignIn") + "', '_self')</script>");
                }
            }
            return View();

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserAccount userAccountModel, FormCollection fc)
        {
            for (int i = 1; i < fc.AllKeys.Length; i++)
            {
                string key = fc.GetKey(i);
                string value = fc.Get(key);
                if (value.Trim().Equals(""))
                    return Content("<script>alert('标记为 * 的输入框不能为空，请重新输入！');window.open('" + Url.Content("~/Account/Register") + "', '_self')</script>");
            }

            if (fc["confirmPassword"].Equals(fc["password"]))
            {
                //UpdateModel<UserAccount>(userAccountModel);
                this.TryUpdateModel<UserAccount>(userAccountModel);
                var users = db.UserAccounts.Where(m => m.UserID == userAccountModel.UserID);
                if (users.Count() == 0)//查无此人
                {
                    if (ModelState.IsValid)
                    {
                        userAccountModel.UserID = fc["userID"];
                        userAccountModel.UserName = fc["userName"];
                        userAccountModel.Password = fc["password"];
                        userAccountModel.UserImage = null;
                        userAccountModel.PhoneNumber = null;
                        userAccountModel.Identification = "user";
                        db.UserAccounts.Add(userAccountModel);
                        //try
                        //{
                            db.SaveChanges();
                        //}
                        //catch (Exception ex)
                        //{
                        //    throw;
                        //}
                        Session["UserID"] = userAccountModel.UserID;
                        Session["UserName"] = userAccountModel.UserName;
                        Session["Identification"] = userAccountModel.Identification;
                        return Content("<script>alert('注册成功！');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('该用户名已存在！');window.open('" + Url.Content("~/Account/Register") + "', '_self')</script>");
                }
            }
            else
                return Content("<script>alert('两次输入的密码不一致！');window.open('" + Url.Content("~/Account/Register") + "', '_self')</script>");

            return View(userAccountModel);
        }

        public ActionResult LogOut()
        {
            Session["UserName"] = null;
            Session["Identification"] = null;
            return Redirect(Url.Content("~/"));
        }
    }
}