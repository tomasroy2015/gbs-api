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
    
    public partial class TB_HotelCreditCardHistory
    {
        public long ID { get; set; }
        public int HotelCreditCardID { get; set; }
        public int HotelID { get; set; }
        public int CreditCardTypeID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public System.DateTime LogDateTime { get; set; }
        public long LogUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual BizTbl_User BizTbl_User1 { get; set; }
        public virtual TB_Hotel TB_Hotel { get; set; }
        public virtual TB_TypeCreditCard TB_TypeCreditCard { get; set; }
    }
}
