using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Comment
    {

        [Key]
        [DisplayName("评论ID")]
        public int CommentID { get; set; }

        [DisplayName("博文ID")]
        [Required]
        public int BlogID { get; set; }

        [DisplayName("评论者ID")]
        public string UserID { get; set; }

        [DisplayName("被回复者ID")]
        public string ReplyID { get; set; }

        [DisplayName("评论内容")]
        [DataType(DataType.MultilineText)]
        public string CommentContent { get; set; }


        [DisplayName("评论时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CommentTime { get; set; }

    }
}