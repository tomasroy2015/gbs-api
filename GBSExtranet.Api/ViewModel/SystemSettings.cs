using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class SystemSettings
    {

        public bool SamedayLateBooking { get; set; }

        public int SamedayLateBookingCount { get; set; }

        public bool SamedayEarlyBooking { get; set; }

        public int SamedayEarlyBookingCount { get; set; }

        public bool NextDayBooking { get; set; }

        public int NextDayBookingCount { get; set; }

        public bool PriorityLateCheckOut { get; set; }

        public bool PriorityEarlyCheckIn { get; set; }

        public bool AirportShuttle { get; set; }

        public bool WelcomeDrink { get; set; }

        public bool FreeBikeRental { get; set; }

        public bool FreeBreakfast { get; set; }

        public bool FreeParking { get; set; }

        public bool FreeWiFi { get; set; }

        public bool RateMissing { get; set; }

        public bool AddressFromGuests { get; set; }

        public bool CCRChecking { get; set; }

        public bool WithoutPhoneNumber { get; set; }

        public bool GracePeriod { get; set; }

        public int GracePeriodHour { get; set; }

        public bool Arrivals { get; set; }

        public bool AuroReplenishment { get; set; }

        public bool SMS { get; set; }

        public int SMSHour { get; set; }

        public bool Gbshotels { get; set; }

        public bool GuestMessage { get; set; }
    }
}