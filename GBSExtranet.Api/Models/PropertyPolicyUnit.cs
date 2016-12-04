//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GBSExtranet.Api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PropertyPolicyUnit
    {
        public PropertyPolicyUnit()
        {
            this.HotelPropertyPolicies = new HashSet<HotelPropertyPolicy>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int PolicyItemID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public Nullable<long> OpUserID { get; set; }
    
        public virtual ICollection<HotelPropertyPolicy> HotelPropertyPolicies { get; set; }
        public virtual PropertyPolicyItem PropertyPolicyItem { get; set; }
    }
}
