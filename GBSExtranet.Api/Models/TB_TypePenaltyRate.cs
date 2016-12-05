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
    
    public partial class TB_TypePenaltyRate
    {
        public TB_TypePenaltyRate()
        {
            this.HotelCancellationPolicies = new HashSet<HotelCancellationPolicy>();
            this.HotelCancellationPolicies1 = new HashSet<HotelCancellationPolicy>();
            this.HotelCancellationPolicies2 = new HashSet<HotelCancellationPolicy>();
            this.TB_BusinessPartnerCancelPolicy = new HashSet<TB_BusinessPartnerCancelPolicy>();
            this.TB_BusinessPartnerCancelPolicyHistory = new HashSet<TB_BusinessPartnerCancelPolicyHistory>();
            this.TB_HotelCancelPolicy = new HashSet<TB_HotelCancelPolicy>();
            this.TB_HotelCancelPolicyHistory = new HashSet<TB_HotelCancelPolicyHistory>();
        }
    
        public int ID { get; set; }
        public int PartID { get; set; }
        public string Name_tr { get; set; }
        public string Name_en { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_ja { get; set; }
        public Nullable<short> Sort { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual ICollection<HotelCancellationPolicy> HotelCancellationPolicies { get; set; }
        public virtual ICollection<HotelCancellationPolicy> HotelCancellationPolicies1 { get; set; }
        public virtual ICollection<HotelCancellationPolicy> HotelCancellationPolicies2 { get; set; }
        public virtual ICollection<TB_BusinessPartnerCancelPolicy> TB_BusinessPartnerCancelPolicy { get; set; }
        public virtual ICollection<TB_BusinessPartnerCancelPolicyHistory> TB_BusinessPartnerCancelPolicyHistory { get; set; }
        public virtual ICollection<TB_HotelCancelPolicy> TB_HotelCancelPolicy { get; set; }
        public virtual ICollection<TB_HotelCancelPolicyHistory> TB_HotelCancelPolicyHistory { get; set; }
        public virtual TB_Part TB_Part { get; set; }
    }
}
