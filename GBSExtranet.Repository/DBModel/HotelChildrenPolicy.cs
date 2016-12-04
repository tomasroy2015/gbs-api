using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class HotelChildrenPolicy
    {
        [Key]
        public int ID { get; set; }
        public int ChildrenPolicyHeaderID { get; set; }
        public int ChildrenPolicyItemID { get; set; }
        public Nullable<int> HotelID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> NoOfExtraBed { get; set; }
        public Nullable<int> NoOfChildExistingBed { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public int ChildUnitID { get; set; }
        public Nullable<bool> IsAttributeSelected { get; set; }
        public Nullable<bool> IsChildrenAccommodated { get; set; }
        public Nullable<bool> IsExtrabedNeeded { get; set; } 
    }
}
