using Blog.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        public ActionResult FirstPage()
        {
            return View();
        }
        // GET: Home
        public ActionResult Index()
        {
            List<Models.Blog> models = db.Blogs.ToList();
            return View();
        }
        public ActionResult Entertainment()
        {
            List<Models.Blog> models = db.Blogs.Where(m=>m.Classification=="娱乐").ToList();
            return View(models);
        }
        public ActionResult Sports()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "体育").ToList();
            return View(models);
        }
        public ActionResult History()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "历史").ToList();
            return View(models);
        }
        public ActionResult Female()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "女性").ToList();
            return View(models);
        }
        public ActionResult Stock()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "股票").ToList();
            return View(models);
        }
        public ActionResult Education()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "教育").ToList();
            return View(models);
        }
        public ActionResult Constellation()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "星座").ToList();
            return View(models);
        }
        public ActionResult Food()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "美食").ToList();
            return View(models);
        }
        public ActionResult Design()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "设计").ToList();
            return View(models);
        }
        public ActionResult BringUp()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "育儿").ToList();
            return View(models);
        }
        public ActionResult Travel()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "旅游").ToList();
            return View(models);
        }
        public ActionResult Collect()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "收藏").ToList();
            return View(models);
        }
        public ActionResult Fashion()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "时尚").ToList();
            return View(models);
        }
        public ActionResult Picture()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "图片").ToList();
            return View(models);
        }
        public ActionResult YueBo()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "悦博").ToList();
            return View(models);
        }
        public ActionResult Address()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "博客地址").ToList();
            return View(models);
        }
    }
}