using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class UserOperation
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string Date { get; set; }
        public string OperationType { get; set; }
        public string Part { get; set; }
        public string RecordID { get; set; }
        public string UserSessionID { get; set; }
        public string IPAddress { get; set; }

        public string UserID { get; set; }
    }
}