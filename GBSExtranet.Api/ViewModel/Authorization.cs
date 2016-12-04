using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class Authorization
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Info { get; set; }
        public string OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string HasRight { get; set; }
        public bool HasValue { get; set; }
    }
}