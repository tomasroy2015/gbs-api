using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class RoomRateAvailability
    {
        public int RoomID { get; set; }
        public int RoomTypeID { get; set; }
        public int MaxPeopleCount { get; set; }
        public int RoomCount { get; set; }
        public string RoomTypeName { get; set; }

        public string CssClass { get; set; }

        //For Date
        public string DateID { get; set; }
        public string Date { get; set; }
        public int DayID { get; set; }
        public int Day { get; set; }
        public int WeekDay { get; set; }
        public string DayName { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }

        //For HotelAvailability
        public int HotelAvailabityID { get; set; }
        public int AvailableRoomCount { get; set; }
        public int MinimumStay { get; set; }
        public bool RoomRateMissing { get; set; }
        public bool CloseToArrival { get; set; }
        public bool CloseToDeparture { get; set; }
        public bool Closed { get; set; }
        public int HotelRateID { get; set; }

        public int PricePolicyTypeID { get; set; }
        public decimal SinglePrice { get; set; }
        public decimal DoublePrice { get; set; }
        public decimal RoomPrice { get; set; }
    }
}