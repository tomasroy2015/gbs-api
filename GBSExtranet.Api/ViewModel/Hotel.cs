using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public int HotelAccommodationTypeID { get; set; }
        public int FirmID { get; set; }

        public int ID { get; set; }
        public string EncryptHotelID { get; set; }
        public string Name { get; set; }
        public string FirmName { get; set; }
        public string HotelTypeName { get; set; }
        public string HotelClassName { get; set; }
        public string HotelAccommodationTypeName { get; set; }
        public string HotelChainName { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
        public string MainRegionName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebAddress { get; set; }
        public string StatusName { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime OpDateTime { get; set; }
        public string CountryID { get; set; }
        public string RegionID { get; set; }
        public string CityID { get; set; }
        public string CurrencyID { get; set; }
        public string RoutingName { get; set; }
        public bool AvailabilityRateUpdate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ParentRegionID { get; set; }
        public int CultureID { get; set; }
        public string HotelTypeID { get; set; }
        public string HotelClassID { get; set; }
        public string HotelChainID { get; set; }
        public string MainRegionID { get; set; }
        public string CityTaxApplied { get; set; }
        public string ClosestAirportID { get; set; }
        public string CountryCode { get; set; }
        public string VAT { get; set; }
        public string ClosestAirportName { get; set; }
        public string ClosestAirportNameWithParentNameAndCode { get; set; }
        public string ClosestAirportDistance { get; set; }
        public string Description { get; set; }
        public string RoomCount { get; set; }
        public string CheckinStart { get; set; }
        public string CheckinEnd { get; set; }
        public string CheckoutStart { get; set; }
        public string CheckoutEnd { get; set; }
        public string FloorCount { get; set; }
        public string BuiltYear { get; set; }
        public string RenovationYear { get; set; }
        public string HitCount { get; set; }
        public string Sort { get; set; }
        public string MapZoomIndex { get; set; }
        public string StatusID { get; set; }
        public bool IsSecret { get; set; }
        public bool IsPreferred { get; set; }
        public string ShowOffline { get; set; }
        public string ChannelManagerID { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }
        public string CreditCardNotRequired { get; set; }
        public string IPAddress { get; set; }
        public List<DropdownList> HotelCreditCardList { get; set; }
        public List<DropdownList> AllCreditCardList { get; set; }
        public List<DropdownList> CheckInFromList { get; set; }
        public List<DropdownList> CheckInToList { get; set; }
        public List<DropdownList> CheckOutFromList { get; set; }
        public List<DropdownList> CheckOutToList { get; set; }
    }
}