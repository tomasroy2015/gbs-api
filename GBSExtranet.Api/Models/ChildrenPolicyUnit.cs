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
    
    public partial class ChildrenPolicyUnit
    {
        public ChildrenPolicyUnit()
        {
            this.ChildrenPolicyItems = new HashSet<ChildrenPolicyItem>();
            this.HotelChildrenPolicies = new HashSet<HotelChildrenPolicy>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Name_en { get; set; }
        public string Name_tr { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_ja { get; set; }
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
        public Nullable<System.DateTime> OpDateTime { get; set; }
    
        public virtual ICollection<ChildrenPolicyItem> ChildrenPolicyItems { get; set; }
        public virtual ICollection<HotelChildrenPolicy> HotelChildrenPolicies { get; set; }
    }
}
