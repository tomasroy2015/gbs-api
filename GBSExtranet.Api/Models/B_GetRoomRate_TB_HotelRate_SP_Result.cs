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
    
    public partial class B_GetRoomRate_TB_HotelRate_SP_Result
    {
        public int HotelRoomID { get; set; }
        public Nullable<decimal> SinglePrice { get; set; }
        public Nullable<decimal> DoublePrice { get; set; }
        public Nullable<decimal> RoomPrice { get; set; }
        public int PricePolicyTypeID { get; set; }
        public Nullable<int> HotelAccommodationTypeID { get; set; }
        public int ID { get; set; }
    }
}
