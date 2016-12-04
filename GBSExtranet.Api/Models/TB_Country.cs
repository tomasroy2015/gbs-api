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
    
    public partial class TB_Country
    {
        public TB_Country()
        {
            this.BizTbl_User = new HashSet<BizTbl_User>();
            this.BizTbl_UserHistory = new HashSet<BizTbl_UserHistory>();
            this.BizTbl_UserSession = new HashSet<BizTbl_UserSession>();
            this.TB_Bank = new HashSet<TB_Bank>();
            this.TB_BankHistory = new HashSet<TB_BankHistory>();
            this.TB_BusinessPartner = new HashSet<TB_BusinessPartner>();
            this.TB_BusinessPartnerHistory = new HashSet<TB_BusinessPartnerHistory>();
            this.TB_Firm = new HashSet<TB_Firm>();
            this.TB_FirmHistory = new HashSet<TB_FirmHistory>();
            this.TB_Hotel = new HashSet<TB_Hotel>();
            this.TB_Hotel1 = new HashSet<TB_Hotel>();
            this.TB_HotelHistory = new HashSet<TB_HotelHistory>();
            this.TB_HotelSearchParameter = new HashSet<TB_HotelSearchParameter>();
            this.TB_HotelSearchParameter1 = new HashSet<TB_HotelSearchParameter>();
            this.TB_IPToNation = new HashSet<TB_IPToNation>();
            this.TB_Message = new HashSet<TB_Message>();
            this.TB_Region = new HashSet<TB_Region>();
            this.TB_Reservation = new HashSet<TB_Reservation>();
            this.TB_ReservationHistory = new HashSet<TB_ReservationHistory>();
        }
    
        public int ID { get; set; }
        public string Name_tr { get; set; }
        public string Name_en { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_ja { get; set; }
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
        public string Code { get; set; }
        public string CultureCode { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<int> VAT { get; set; }
        public Nullable<bool> HasCityTax { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<short> Sort { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public Nullable<int> TempCode { get; set; }
        public string ContinentsID { get; set; }
    
        public virtual ICollection<BizTbl_User> BizTbl_User { get; set; }
        public virtual BizTbl_User BizTbl_User1 { get; set; }
        public virtual ICollection<BizTbl_UserHistory> BizTbl_UserHistory { get; set; }
        public virtual ICollection<BizTbl_UserSession> BizTbl_UserSession { get; set; }
        public virtual ICollection<TB_Bank> TB_Bank { get; set; }
        public virtual ICollection<TB_BankHistory> TB_BankHistory { get; set; }
        public virtual ICollection<TB_BusinessPartner> TB_BusinessPartner { get; set; }
        public virtual ICollection<TB_BusinessPartnerHistory> TB_BusinessPartnerHistory { get; set; }
        public virtual TB_Currency TB_Currency { get; set; }
        public virtual ICollection<TB_Firm> TB_Firm { get; set; }
        public virtual ICollection<TB_FirmHistory> TB_FirmHistory { get; set; }
        public virtual ICollection<TB_Hotel> TB_Hotel { get; set; }
        public virtual ICollection<TB_Hotel> TB_Hotel1 { get; set; }
        public virtual ICollection<TB_HotelHistory> TB_HotelHistory { get; set; }
        public virtual ICollection<TB_HotelSearchParameter> TB_HotelSearchParameter { get; set; }
        public virtual ICollection<TB_HotelSearchParameter> TB_HotelSearchParameter1 { get; set; }
        public virtual ICollection<TB_IPToNation> TB_IPToNation { get; set; }
        public virtual ICollection<TB_Message> TB_Message { get; set; }
        public virtual ICollection<TB_Region> TB_Region { get; set; }
        public virtual ICollection<TB_Reservation> TB_Reservation { get; set; }
        public virtual ICollection<TB_ReservationHistory> TB_ReservationHistory { get; set; }
    }
}
