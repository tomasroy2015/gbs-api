using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class ChildrenPolicyViewModel
    {
        public int ID { get; set; }
        public bool HasChildrenPolicy { get; set; }
        public bool HasExtraBedPolicy { get; set; }
        public Nullable<int> MaxNoExtraBed { get; set; }
        public Nullable<int> MaxNoOfChild { get; set; }
        public Nullable<decimal> ChildrenPrice { get; set; }
        public Nullable<decimal> AdultPrice { get; set; }
        public Nullable<int> HotelID { get; set; }
        public string Description { get; set; }
    }
}