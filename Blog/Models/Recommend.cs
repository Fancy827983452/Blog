using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Recommend
    {
        [Key]
        [DisplayName("推荐ID")]
        public int RecommendID { get; set; }

        [DisplayName("博文ID")]
        public int BlogID { get; set; }

        [DisplayName("博文标题")]
        public string BlogTitle { get; set; }

        [DisplayName("推荐时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
    }
}