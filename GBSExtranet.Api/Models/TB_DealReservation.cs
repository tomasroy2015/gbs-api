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
    
    public partial class TB_DealReservation
    {
        public TB_DealReservation()
        {
            this.TB_DealReservationPromotion = new HashSet<TB_DealReservationPromotion>();
        }
    
        public long ID { get; set; }
        public long ReservationID { get; set; }
        public Nullable<int> FirmID { get; set; }
        public int DealID { get; set; }
        public string GuestFullName { get; set; }
        public string PromotionCode { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<int> PeopleCount { get; set; }
        public Nullable<int> BusinessPartnerCancelPolicyID { get; set; }
        public Nullable<bool> NonRefundable { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public int GeneralPromotionDiscountPercentage { get; set; }
        public int PromotionDiscountPercentage { get; set; }
        public Nullable<decimal> PayableAmount { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> CostCurrencyID { get; set; }
        public Nullable<short> ComissionRate { get; set; }
        public Nullable<decimal> ComissionAmount { get; set; }
        public Nullable<int> ComissionCurrencyID { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<int> DepositTypeID { get; set; }
        public Nullable<int> DepositCurrencyID { get; set; }
        public Nullable<decimal> DepositInTL { get; set; }
        public Nullable<decimal> ChargedAmount { get; set; }
        public Nullable<int> ChargedAmountCurrencyID { get; set; }
        public Nullable<System.DateTime> ChargeDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> ReservationOperationID { get; set; }
        public Nullable<System.DateTime> CancelDateTime { get; set; }
        public Nullable<bool> Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual TB_BusinessPartnerCancelPolicy TB_BusinessPartnerCancelPolicy { get; set; }
        public virtual TB_Currency TB_Currency { get; set; }
        public virtual TB_Currency TB_Currency1 { get; set; }
        public virtual TB_Currency TB_Currency2 { get; set; }
        public virtual TB_Currency TB_Currency3 { get; set; }
        public virtual TB_Currency TB_Currency4 { get; set; }
        public virtual TB_Deal TB_Deal { get; set; }
        public virtual TB_Firm TB_Firm { get; set; }
        public virtual TB_Reservation TB_Reservation { get; set; }
        public virtual TB_TypeDeposit TB_TypeDeposit { get; set; }
        public virtual TB_TypeReservationOperation TB_TypeReservationOperation { get; set; }
        public virtual TB_TypeReservationStatus TB_TypeReservationStatus { get; set; }
        public virtual ICollection<TB_DealReservationPromotion> TB_DealReservationPromotion { get; set; }
    }
}
