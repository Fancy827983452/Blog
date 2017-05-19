using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Focus
    {

        [Key]
        [DisplayName("关注ID")]
        public int FocusID { get; set; }

        [DisplayName("被关注者")]
        public string Focused { get; set; }

        [DisplayName("关注者")]
        public string DoFocus { get; set; }
    }
}
