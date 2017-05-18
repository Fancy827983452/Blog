using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class UserAccount
    {
        [Key]
        [DisplayName("用户ID")]
        public string UserID { get; set; }

        [DisplayName("用户名")]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("头像")]
        public string UserImage { get; set; }

        [DisplayName("手机号")]
        public string PhoneNumber { get; set; }

        [DisplayName("身份")]
        public string Identification { get; set; }

        [DisplayName("状态")]
        public bool status { get; set; }//true--活动，false--锁定
    }
}