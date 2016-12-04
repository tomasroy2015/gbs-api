using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class Parameter
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsCommon { get; set; }
        public string Date { get; set; }
        public string Operation { get; set; }
        public long OpUserID { get; set; }
    }
}