using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace GBSExtranet.Api.ViewModel
{
    public class HotelRateAvailabilityViewModel
    {
        public string DisplayResult { get; set; }
        public List<HotelRateAvailability> HotelAvailabilityList { get; set; }
        public List<HotelRateAvailability> Day { get; set; }
        public List<HotelRateAvailability> CloseOpenAvailabilityDay {get;set;}
        public List<HotelRateAvailability> Room { get; set; }
        public List<HotelRateAvailability> RoomDay { get; set; }
        public List<HotelRateAvailability> RefundablePrices { get; set; }
        public List<HotelRateAvailability> NonRefundablePrices { get; set; }
        public Hashtable DateAvailability { get; set; }
        public int ColSpan { get; set; }
        public object Month1 { get; set; }
        public int Month2ColSpan { get; set; }
        public object Month2 { get; set; }
    }
}