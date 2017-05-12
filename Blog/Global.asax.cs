using Blog.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //在没有数据库时默认创建一个数据库
            //Database.SetInitializer(new CreateDatabaseIfNotExists<BlogDbContext>());

            //在模型改变时自动重新创建一个新的数据库
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BlogDbContext>());
            using (var context = new BlogDbContext())
            {
                context.Database.Initialize(true);
            }
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
