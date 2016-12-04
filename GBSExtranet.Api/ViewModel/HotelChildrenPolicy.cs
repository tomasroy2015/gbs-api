using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class HotelChildrenPolicy
    {
        public int ID { get; set; }
        public int ChildrenPolicyHeaderID { get; set; }
        public int ChildrenPolicyItemID { get; set; }
        public Nullable<int> HotelID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> NoOfExtraBed { get; set; }
        public Nullable<int> NoOfChildExistingBed { get; set; }
        public int CurrencyID { get; set; }
        public int ChildUnitID { get; set; }
    }
}