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
    
    public partial class TB_HotelRate
    {
        public int ID { get; set; }
        public int HotelRoomID { get; set; }
        public int DateID { get; set; }
        public int PricePolicyTypeID { get; set; }
        public Nullable<int> HotelAccommodationTypeID { get; set; }
        public Nullable<decimal> SinglePrice { get; set; }
        public Nullable<decimal> DoublePrice { get; set; }
        public Nullable<decimal> RoomPrice { get; set; }
        public int CurrencyID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual BizTbl_User BizTbl_User1 { get; set; }
        public virtual BizTbl_User BizTbl_User2 { get; set; }
        public virtual TB_Currency TB_Currency { get; set; }
        public virtual TB_Currency TB_Currency1 { get; set; }
        public virtual TB_Currency TB_Currency2 { get; set; }
        public virtual TB_Date TB_Date { get; set; }
        public virtual TB_Date TB_Date1 { get; set; }
        public virtual TB_Date TB_Date2 { get; set; }
        public virtual TB_HotelRoom TB_HotelRoom { get; set; }
        public virtual TB_TypePricePolicy TB_TypePricePolicy { get; set; }
        public virtual TB_HotelRoom TB_HotelRoom1 { get; set; }
        public virtual TB_TypeHotelAccommodation TB_TypeHotelAccommodation { get; set; }
        public virtual TB_HotelRoom TB_HotelRoom2 { get; set; }
    }
}
