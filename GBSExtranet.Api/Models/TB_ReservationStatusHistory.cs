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
    
    public partial class TB_ReservationStatusHistory
    {
        public long ID { get; set; }
        public long ReservationID { get; set; }
        public int StatusID { get; set; }
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual TB_Reservation TB_Reservation { get; set; }
        public virtual TB_TypeReservationStatus TB_TypeReservationStatus { get; set; }
    }
}
