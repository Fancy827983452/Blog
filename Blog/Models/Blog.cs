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
        [Required]
        public string BlogID { get; set; }

        [DisplayName("博文标题")]
        [Required]
        public string BlogTitle { get; set; }

        [DisplayName("博文内容")]
        [Required]
        public string BlogContent { get; set; }

        [DisplayName("博文分类")]
        public string Classification { get; set; }

        [DisplayName("博主ID")]
        public string BloggerID { get; set; }

        [DisplayName("创建时间")]
        [DataType(DataType.DateTime)]
        public string CreateTime { get; set; }

        [DisplayName("修改时间")]
        [DataType(DataType.DateTime)]
        public string ModifyTime { get; set; }
    }
}