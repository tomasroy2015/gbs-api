using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class ChildrenPolicyItem
    {
        public int ID { get; set; }
        public int ChildrenPolicyHeaderID { get; set; }
        public string Name { get; set; }       
        public string Description { get; set; }      
        public Nullable<bool> IsChargeable { get; set; }
        public string PriceLabel { get; set; }
        public int ChildUnitID { get; set; }
        public Nullable<bool> IsProviderNeeded { get; set; }
        public Nullable<bool> HasChildUnit { get; set; }
        public Nullable<bool> IsExtrabedItem { get; set; }
        public Nullable<bool> IsCheckedItem { get; set; }
        public Nullable<bool> IsExistingBedItem { get; set; }
        public Nullable<int> HotelID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> NoOfExtraBed { get; set; }
        public Nullable<int> NoOfChildExistingBed { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<bool> IsAttributeSelected { get; set; }
        public Nullable<bool> IsChildrenAccommodated { get; set; }
        public Nullable<bool> IsExtrabedNeeded { get; set; } 
    }
}