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
            List<Models.Recommend> models = db.Recommends.ToList();
            return View(models);
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
        public ActionResult Military()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "军事").ToList();
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
        public ActionResult Health()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "健康").ToList();
            return View(models);
        }
        public ActionResult Social()
        {
            List<Models.Blog> models = db.Blogs.Where(m => m.Classification == "社会").ToList();
            return View(models);
        }
    }
}