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
    
    public partial class TB_SP_GetReservationCC_Result
    {
        public long ReservationID { get; set; }
        public Nullable<int> CCTypeID { get; set; }
        public string CCTypeName { get; set; }
        public string CCFullName { get; set; }
        public string CCNo { get; set; }
        public string CCExpiration { get; set; }
        public string CCCVC { get; set; }
    }
}
