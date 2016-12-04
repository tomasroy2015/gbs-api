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
    
    public partial class TB_TransferReservation
    {
        public TB_TransferReservation()
        {
            this.TB_TransferReservationPromotion = new HashSet<TB_TransferReservationPromotion>();
        }
    
        public long ID { get; set; }
        public long ReservationID { get; set; }
        public Nullable<int> FirmID { get; set; }
        public Nullable<int> TransferID { get; set; }
        public Nullable<System.DateTime> TransferDate { get; set; }
        public string TransferTime { get; set; }
        public string GuestFullName { get; set; }
        public string TransferAddress { get; set; }
        public Nullable<short> PassangerCount { get; set; }
        public Nullable<int> VehicleTypeID { get; set; }
        public string FlightNumber { get; set; }
        public Nullable<bool> ReturnTransfer { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public string ReturnTime { get; set; }
        public string ReturnFlightNumber { get; set; }
        public Nullable<int> BusinessPartnerCancelPolicyID { get; set; }
        public Nullable<bool> NonRefundable { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> GeneralPromotionDiscountPercentage { get; set; }
        public Nullable<int> PromotionDiscountPercentage { get; set; }
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
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public Nullable<long> OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual TB_BusinessPartnerCancelPolicy TB_BusinessPartnerCancelPolicy { get; set; }
        public virtual TB_Currency TB_Currency { get; set; }
        public virtual TB_Currency TB_Currency1 { get; set; }
        public virtual TB_Currency TB_Currency2 { get; set; }
        public virtual TB_Currency TB_Currency3 { get; set; }
        public virtual TB_Currency TB_Currency4 { get; set; }
        public virtual TB_Firm TB_Firm { get; set; }
        public virtual TB_Reservation TB_Reservation { get; set; }
        public virtual TB_Transfer TB_Transfer { get; set; }
        public virtual TB_TypeVehicle TB_TypeVehicle { get; set; }
        public virtual TB_TypeDeposit TB_TypeDeposit { get; set; }
        public virtual TB_TypeReservationOperation TB_TypeReservationOperation { get; set; }
        public virtual TB_TypeReservationStatus TB_TypeReservationStatus { get; set; }
        public virtual ICollection<TB_TransferReservationPromotion> TB_TransferReservationPromotion { get; set; }
    }
}
