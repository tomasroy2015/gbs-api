using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class Promotion
    {
        public int ID { get; set; }
        public int HotelID { get; set; }
        public int PromotionID { get; set; }
        public string PromotionType { get; set; }
        public string PromotionName { get; set; }
        public string PromotionDescription { get; set; }
        public int PromotionSort { get; set; }
        public bool HasDiscount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AccommodationStartDate { get; set; }
        public DateTime AccommodationEndDate { get; set; }
        public string DiscountPercentage { get; set; }
        public int DayCount { get; set; }
        public int DayID { get; set; }
        public string DayName { get; set; }
        public string PricePolicyName { get; set; }
        public int PricePolicyID { get; set; }
        public bool SecretDeal { get; set; }
        public bool ValidForAllRoomTypes { get; set; }
        public bool Active { get; set; }
        public int EarlyBookerMargin { get; set; }
        public int LastMinuteMargin { get; set; }
        public DateTime BookingDate { get; set; }
        public string Region { get; set; }
        public string Alltypes { get; set; }
        public string RoomNames { get; set; }
        public string DiscountText { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class NewPromotion
    {
        public int ID { get; set; }
        public int PartID { get; set; }
        public string PromotionType { get; set; }
        public string PromotionName { get; set; }
        public string PromotionDescription { get; set; }
        public int PromotionSort { get; set; }
        public bool GeneralPromotion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TargetStartDate { get; set; }
        public DateTime TargetEndDate { get; set; }
        public string DiscountPercentage { get; set; }
        public string Count { get; set; }
        public string RegionID { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
    }

    public class PromotionInsert
    {
       public int  PromotionID{get;set;}
       public int DiscountPercentage{ get; set; }
       public bool HasDiscount{ get; set; }
       public string AccommodationStartDate{ get; set; }
       public string  AccommodationEndDate{ get; set; }
       public int? DayCount{ get; set; }
       public int? EarlyBookerMargin{ get; set; }
       public int? LastMinuteMargin{ get; set; }
       public string BookingDate{ get; set; }
       public bool ValidForAllRoomTypes{ get; set; }
       public bool SecretDeal { get; set; }
    }
}