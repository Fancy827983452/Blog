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
       

    }
}