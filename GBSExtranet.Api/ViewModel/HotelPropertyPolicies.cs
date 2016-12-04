using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class HotelPropertyPolicies
    {
        public int ID { get; set; }
        public int PropertyPolicyID { get; set; }
        public int PropertyPolicyItemID { get; set; }
        public int HotelID { get; set; }
        public int UnitID { get; set; }
        public string UnitValue { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int CurrencyID { get; set; }
        public bool Active { get; set; }
    }
}