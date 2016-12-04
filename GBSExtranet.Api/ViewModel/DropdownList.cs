using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class DropdownList
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } 
        public string TimeID { get; set; }

        public long PartID { get; set; }
    }
}