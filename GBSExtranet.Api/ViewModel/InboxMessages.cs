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
    }
}