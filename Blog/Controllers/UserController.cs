using Blog.DatabaseContext;
using Blog.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult Write(Models.Blog BlogModel, FormCollection fc)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo([Bind(Include = "UserID,UserName,Password,UserImage,PhoneNumber,Identification")]UserAccount userAccountModel,FormCollection fc, HttpPostedFileBase file)
        {
            if (fc["username"].ToString().Trim().Equals(""))
                return Content("<script>alert('标记为 * 的输入框不能为空，请重新输入！');window.open('" + Url.Content("~/User/EditInfo") + "', '_self')</script>");
            else
            {
                //this.TryUpdateModel<UserAccount>(userAccountModel);
                var name = db.UserAccounts.Where(m => m.UserName == userAccountModel.UserName);
                if (name.Count() > 0)
                    return Content("<script>alert('该用户昵称已存在！');window.open('" + Url.Content("~/User/EditInfo") + "', '_self')</script>");
                else
                {
                    userAccountModel = db.UserAccounts.Find(Session["UserID"]);
                    string password = userAccountModel.Password;
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
                            catch(Exception e)
                            {
                                return Content("上传异常 ！", "text/plain");
                            }

                            db.UserAccounts.Remove(userAccountModel);
                            db.SaveChanges();
                            userAccountModel.UserID = Session["UserID"].ToString();
                            userAccountModel.UserName = fc["userName"];
                            userAccountModel.Password = password;
                            userAccountModel.UserImage = file.FileName;//仅仅保存图片的名字
                            userAccountModel.PhoneNumber = fc["tel"];
                            userAccountModel.Identification = "user";
                            db.UserAccounts.Add(userAccountModel);
                            db.SaveChanges();

                            Session["UserName"] = userAccountModel.UserName;
                            Session["PhoneNumber"] = userAccountModel.PhoneNumber;
                            Session["Identification"] = userAccountModel.Identification;
                            return Content("<script>alert('修改个人信息成功！');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                        }
                        else
                            return Content("没有文件！", "text/plain");
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