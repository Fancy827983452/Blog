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
        public int LikeID { get; set; } //自增的id

        [DisplayName("博文ID")]
        public int BlogID { get; set; }

        [DisplayName("用户ID")]
        public string UserID { get; set; }

        [DisplayName("点赞时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [DisplayName("状态")]
        public bool status { get; set; } //true--有效赞，false--已取消赞

    }
}