using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Like
    {

        [Key]
        [DisplayName("点赞ID")]
        public int LikeID { get; set; }
        
        [DisplayName("博文ID")]
        public int BlogID { get; set; }
       
        [DisplayName("点赞用户ID")]
        public string UserID { get; set; }

       
    }
}