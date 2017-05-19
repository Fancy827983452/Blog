using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Blog
    {
        [Key]
        [DisplayName("博文ID")]
        public int BlogID { get; set; }

        [DisplayName("博文标题")]
        [Required]
        public string BlogTitle { get; set; }

        [DisplayName("博文内容")]
        [DataType(DataType.MultilineText)]
        public string BlogContent { get; set; }

        [DisplayName("博文分类")]
        public string Classification { get; set; }

        [DisplayName("博主ID")]
        public string BloggerID { get; set; }

        [DisplayName("创建时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [DisplayName("修改时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ModifyTime { get; set; }

        [DisplayName("推荐状态")]
        public bool isRecommended { get; set; }//true--已推荐，false--取消推荐
    }
}