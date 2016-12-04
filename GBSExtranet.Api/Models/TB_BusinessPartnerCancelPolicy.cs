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
    
    public partial class TB_BusinessPartnerCancelPolicy
    {
        public TB_BusinessPartnerCancelPolicy()
        {
            this.TB_Deal = new HashSet<TB_Deal>();
            this.TB_DealHistory = new HashSet<TB_DealHistory>();
            this.TB_DealReservation = new HashSet<TB_DealReservation>();
            this.TB_DealReservationHistory = new HashSet<TB_DealReservationHistory>();
            this.TB_Tour = new HashSet<TB_Tour>();
            this.TB_TourHistory = new HashSet<TB_TourHistory>();
            this.TB_TourReservation = new HashSet<TB_TourReservation>();
            this.TB_TransferReservation = new HashSet<TB_TransferReservation>();
            this.TB_TransferReservationHistory = new HashSet<TB_TransferReservationHistory>();
        }
    
        public int ID { get; set; }
        public int BusinessPartnerID { get; set; }
        public int PartID { get; set; }
        public int CancelTypeID { get; set; }
        public Nullable<int> RefundableDayCount { get; set; }
        public Nullable<int> PenaltyRateTypeID { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual TB_BusinessPartner TB_BusinessPartner { get; set; }
        public virtual TB_Part TB_Part { get; set; }
        public virtual TB_TypeCancel TB_TypeCancel { get; set; }
        public virtual TB_TypePenaltyRate TB_TypePenaltyRate { get; set; }
        public virtual ICollection<TB_Deal> TB_Deal { get; set; }
        public virtual ICollection<TB_DealHistory> TB_DealHistory { get; set; }
        public virtual ICollection<TB_DealReservation> TB_DealReservation { get; set; }
        public virtual ICollection<TB_DealReservationHistory> TB_DealReservationHistory { get; set; }
        public virtual ICollection<TB_Tour> TB_Tour { get; set; }
        public virtual ICollection<TB_TourHistory> TB_TourHistory { get; set; }
        public virtual ICollection<TB_TourReservation> TB_TourReservation { get; set; }
        public virtual ICollection<TB_TransferReservation> TB_TransferReservation { get; set; }
        public virtual ICollection<TB_TransferReservationHistory> TB_TransferReservationHistory { get; set; }
    }
}
