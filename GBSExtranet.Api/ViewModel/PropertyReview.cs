using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyReview
    {
        public int HotelID { get; set; }
        public decimal AveragePoint { get; set; }
        public int ReviewTypeID { get; set; }
        public string ReviewTypeName { get; set; }
        public int sort { get; set; }
        public decimal PointSum { get; set; }
        public decimal FirstAveragePoint { get; set; }
        public int ReviewCount { get; set; }
        public string ReviewTypeEvaluationName { get; set; }
        public string AvgPropertyPoint { get; set; }
        public double height { get; set; }
        public string height1 { get; set; }
        public double width { get; set; }
        public string width1 { get; set; }
        public double width2 { get; set; }
        public string width3 { get; set; }
        public string FirstReviewTypeEvaluationName { get; set; }
        public int TotalRecordCount { get; set; }
        public int ReservationID { get; set; }
        public int TravellerTypeID { get; set; }
        public string TravelerTypeName { get; set; }
        public string Review { get; set; }
        public int ReviewStatusID { get; set; }
        public string ReviewStatusName { get; set; }
        public bool Anonymous { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime OpDateTime { get; set; }
        public string IPAddress { get; set; }
        public decimal Point { get; set; }
        public string ReviewTypeScaleName { get; set; }
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public int PartID { get; set; }
        public string Part { get; set; }
        public int FirmID { get; set; }
        public string FirmName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public int ReviewTypeCount { get; set; }
        public float ReviewCount1 { get; set; }
        public string ReviewInfo { get; set; }

        public int ReservationReviewID { get; set; }

        public int ReviewTypeCount1 { get; set; }
        public string PositiveReview { get; set; }
        public string NegativeReview { get; set; }
    }
}