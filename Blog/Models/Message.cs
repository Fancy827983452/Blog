using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Message
    {

        [Key]
        [DisplayName("留言ID")]
        public int MessageID { get; set; }

        [DisplayName("发送方")]       
        public String SenderID { get; set; }

        [DisplayName("接收方")]
        public string ReceiverID { get; set; }      

        [DisplayName("留言内容")]
        [DataType(DataType.MultilineText)]
        public string MessageContent { get; set; }


        [DisplayName("留言时间")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime LeaveTime { get; set; }
    }
}