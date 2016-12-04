using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class HotelRateAvailability
    {
        public int ID { get; set; }
        public Int64 DateID { get; set; }
        public DateTime Date { get; set; }
        public int DayID { get; set; }
        public int Day { get; set; }
        public int WeekDay { get; set; }
        public string DayName { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int HotelRoomID { get; set; }
        public int HotelID { get; set; }
        public int AvailableRoomCount { get; set; }
        public int RoomCount { get; set; }
        public int CloseToArrival { get; set; }
        public int CloseToDeparture { get; set; }
        public int MinimumStay { get; set; }
        public int Closed { get; set; }
        public int RoomRateMissing { get; set; }
        public int RoomTypeID { get; set; }
        public bool IsValidForOpenClose { get; set; }
        public string RoomTypeName { get; set; }
        public int RoomSize { get; set; }
        public int MaxPeopleCount { get; set; }
        public string IDWithMaxPeopleCount { get; set; }
        public int MaxChildrenCount { get; set; }

        public int BabyCotCount { get; set; }

        public int ExtraBedCount { get; set; }

        public string SmokingTypeID { get; set; }
        public string SmokingTypeName { get; set; }

        public string ViewTypeID { get; set; }
        public string ViewTypeName { get; set; }
        public string IncludedInRoomTypeCaption { get; set; }


        public string hotelRoomAvailabilityText { get; set; }

        public string lbtnAvailableRoomCount { get; set; }

        public string HotelAvailableStatus { get; set; }

        public string EncryptHotelRoomID { get; set; }
        public string PricePolicyType { get; set; }
       
        public decimal RoomPrice { get; set; }
    }
}