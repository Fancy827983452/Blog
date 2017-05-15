using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.DatabaseContext
{
    public class BlogDbContext : System.Data.Entity.DbContext
    {
        public BlogDbContext()
            : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Blog.Models.UserAccount> UserAccounts { get; set; }

        public System.Data.Entity.DbSet<Blog.Models.Blog> Blogs { get; set; }
    }
}