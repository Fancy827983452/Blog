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
        [Required]
        public string UserID { get; set; }

        [DisplayName("用户名")]
        [Required]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("头像")]
        public byte[] UserImage { get; set; }

        [DisplayName("手机号")]
        public string PhoneNumber { get; set; }

        [DisplayName("身份")]
        public string Identification { get; set; }
    }
}