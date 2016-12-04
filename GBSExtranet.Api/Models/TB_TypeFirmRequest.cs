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
    
    public partial class TB_TypeFirmRequest
    {
        public TB_TypeFirmRequest()
        {
            this.TB_FirmRequest = new HashSet<TB_FirmRequest>();
            this.TB_FirmRequestHistory = new HashSet<TB_FirmRequestHistory>();
        }
    
        public int ID { get; set; }
        public Nullable<int> PartID { get; set; }
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
        public virtual ICollection<TB_FirmRequest> TB_FirmRequest { get; set; }
        public virtual ICollection<TB_FirmRequestHistory> TB_FirmRequestHistory { get; set; }
        public virtual TB_Part TB_Part { get; set; }
    }
}
