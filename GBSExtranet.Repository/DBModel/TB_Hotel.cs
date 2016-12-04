using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class TB_Hotel
    {
        [Key]
        public int ID { get; set; }
        public int FirmID { get; set; }
        public int HotelTypeID { get; set; }
        public int HotelClassID { get; set; }
        public Nullable<int> HotelChainID { get; set; }
        public int HotelAccommodationTypeID { get; set; }
        public int CountryID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<long> RegionID { get; set; }
        public Nullable<long> MainRegionID { get; set; }
        public string Name { get; set; }
        public string Description_tr { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_es { get; set; }
        public string Description_fr { get; set; }
        public string Description_ru { get; set; }
        public string Description_it { get; set; }
        public string Description_ar { get; set; }
        public string Description_ja { get; set; }
        public string Description_pt { get; set; }
        public string Description_zh { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PostCode { get; set; }
        public Nullable<short> RoomCount { get; set; }
        public string WebAddress { get; set; }
        public string Email { get; set; }
        public string CheckinStart { get; set; }
        public string CheckinEnd { get; set; }
        public string CheckoutStart { get; set; }
        public string CheckoutEnd { get; set; }
        public Nullable<int> FloorCount { get; set; }
        public Nullable<int> BuiltYear { get; set; }
        public Nullable<int> RenovationYear { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<bool> IsPreferred { get; set; }
        public Nullable<bool> IsMainPageDisplay { get; set; }
        public Nullable<int> MainPageDisplaySort { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<int> MapZoomIndex { get; set; }
        public Nullable<long> ClosestAirportID { get; set; }
        public Nullable<decimal> ClosestAirportDistance { get; set; }
        public Nullable<decimal> ReviewPoint { get; set; }
        public Nullable<bool> ShowOffline { get; set; }
        public Nullable<bool> CreditCardNotRequired { get; set; }
        public Nullable<int> CultureID { get; set; }
        public string RoutingName { get; set; }
        public Nullable<bool> IsSecret { get; set; }
        public Nullable<bool> IsValueDeal { get; set; }
        public Nullable<System.DateTime> LatestBookingDate { get; set; }
        public Nullable<int> LatestBookingCountryID { get; set; }
        public Nullable<int> ChannelManagerID { get; set; }
        public Nullable<bool> AvailabilityRateUpdate { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public Nullable<long> CreateUserID { get; set; }
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public Nullable<long> OpUserID { get; set; }
        public string IPAddress { get; set; }
    }
}
