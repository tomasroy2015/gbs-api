using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class InboxMessages
    {
        public int MessageID { get; set; }

        public string Subject { get; set; }

        public string CreatedDate { get; set; }

        public string TotalMsg { get; set; }

        public string Unreadmsg { get; set; }

        public string ReadStatus { get; set; }

        public int ID { get; set; }

        public string Email { get; set; }

        public string HotelName { get; set; }

        public string UserName { get; set; }

        public string MessageInfo { get; set; }

        public int ReceiverUserID { get; set; }

        public int SenderUserID { get; set; }

        public string CreatedTime { get; set; }

        public int ReplySenderUserID { get; set; }
    }
}