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
    
    public partial class TB_HotelSearch
    {
        public long ID { get; set; }
        public long SearchParameterID { get; set; }
        public int HotelID { get; set; }
        public long HotelRoomID { get; set; }
        public int MaxPeopleCount { get; set; }
        public int MaxChildrenCount { get; set; }
        public bool ChildrenAllowed { get; set; }
        public decimal MinumumRoomRate { get; set; }
        public decimal MinumumRoomRateHistory { get; set; }
        public decimal TotalRoomRate { get; set; }
        public decimal TotalRoomRateHistory { get; set; }
        public int AvailableRoomCount { get; set; }
        public int AllocatedRoomCount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual TB_Hotel TB_Hotel { get; set; }
        public virtual TB_HotelSearchParameter TB_HotelSearchParameter { get; set; }
    }
}
