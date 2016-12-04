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
    
    public partial class TB_HotelAttribute
    {
        public int ID { get; set; }
        public int HotelID { get; set; }
        public int AttributeID { get; set; }
        public bool Charged { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string UnitValue { get; set; }
        public Nullable<decimal> Charge { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual TB_Attribute TB_Attribute { get; set; }
        public virtual TB_Currency TB_Currency { get; set; }
        public virtual TB_Hotel TB_Hotel { get; set; }
        public virtual TB_Unit TB_Unit { get; set; }
    }
}
