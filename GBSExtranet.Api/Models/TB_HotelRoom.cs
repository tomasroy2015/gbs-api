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
    
    public partial class TB_HotelRoom
    {
        public TB_HotelRoom()
        {
            this.TB_HotelAvailability = new HashSet<TB_HotelAvailability>();
            this.TB_HotelAvailabilityHistory = new HashSet<TB_HotelAvailabilityHistory>();
            this.TB_HotelPromotionRoom = new HashSet<TB_HotelPromotionRoom>();
            this.TB_HotelRate = new HashSet<TB_HotelRate>();
            this.TB_HotelRate1 = new HashSet<TB_HotelRate>();
            this.TB_HotelRate2 = new HashSet<TB_HotelRate>();
            this.TB_HotelRateHistory = new HashSet<TB_HotelRateHistory>();
            this.TB_HotelReservation = new HashSet<TB_HotelReservation>();
            this.TB_HotelReservationHistory = new HashSet<TB_HotelReservationHistory>();
            this.TB_HotelRoom11 = new HashSet<TB_HotelRoom>();
            this.TB_HotelRoomAttribute = new HashSet<TB_HotelRoomAttribute>();
            this.TB_HotelRoomAttributeHistory = new HashSet<TB_HotelRoomAttributeHistory>();
            this.TB_HotelRoomBed = new HashSet<TB_HotelRoomBed>();
            this.TB_HotelRoomBedHistory = new HashSet<TB_HotelRoomBedHistory>();
        }
    
        public int ID { get; set; }
        public int HotelID { get; set; }
        public string Description_tr { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_es { get; set; }
        public string Description_fr { get; set; }
        public string Description_ru { get; set; }
        public string Description_it { get; set; }
        public string Description_ar { get; set; }
        public string Description_jp { get; set; }
        public int RoomTypeID { get; set; }
        public int RoomCount { get; set; }
        public int RoomSize { get; set; }
        public short MaxPeopleCount { get; set; }
        public Nullable<short> MaxChildrenCount { get; set; }
        public short BabyCotCount { get; set; }
        public short ExtraBedCount { get; set; }
        public Nullable<int> SmokingTypeID { get; set; }
        public Nullable<int> ViewTypeID { get; set; }
        public Nullable<bool> Promotion { get; set; }
        public Nullable<int> RelatedHotelRoomID { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<bool> Active { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public long CreateUserID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string Language { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual BizTbl_User BizTbl_User1 { get; set; }
        public virtual TB_Hotel TB_Hotel { get; set; }
        public virtual ICollection<TB_HotelAvailability> TB_HotelAvailability { get; set; }
        public virtual ICollection<TB_HotelAvailabilityHistory> TB_HotelAvailabilityHistory { get; set; }
        public virtual ICollection<TB_HotelPromotionRoom> TB_HotelPromotionRoom { get; set; }
        public virtual ICollection<TB_HotelRate> TB_HotelRate { get; set; }
        public virtual ICollection<TB_HotelRate> TB_HotelRate1 { get; set; }
        public virtual ICollection<TB_HotelRate> TB_HotelRate2 { get; set; }
        public virtual ICollection<TB_HotelRateHistory> TB_HotelRateHistory { get; set; }
        public virtual ICollection<TB_HotelReservation> TB_HotelReservation { get; set; }
        public virtual ICollection<TB_HotelReservationHistory> TB_HotelReservationHistory { get; set; }
        public virtual TB_HotelRoom TB_HotelRoom1 { get; set; }
        public virtual TB_HotelRoom TB_HotelRoom2 { get; set; }
        public virtual ICollection<TB_HotelRoom> TB_HotelRoom11 { get; set; }
        public virtual TB_HotelRoom TB_HotelRoom3 { get; set; }
        public virtual TB_TypeRoom TB_TypeRoom { get; set; }
        public virtual TB_TypeSmoking TB_TypeSmoking { get; set; }
        public virtual TB_TypeView TB_TypeView { get; set; }
        public virtual ICollection<TB_HotelRoomAttribute> TB_HotelRoomAttribute { get; set; }
        public virtual ICollection<TB_HotelRoomAttributeHistory> TB_HotelRoomAttributeHistory { get; set; }
        public virtual ICollection<TB_HotelRoomBed> TB_HotelRoomBed { get; set; }
        public virtual ICollection<TB_HotelRoomBedHistory> TB_HotelRoomBedHistory { get; set; }
    }
}
