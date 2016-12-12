using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyReservation
    {
        public string SelectedBedText { get; set; }
        public List<PropertyReservation> RoomRateDetailslist { get; set; }
        public List<string> PropertyConditionslist { get; set; }
        public double ComissionSum { get; set; }
        public double ComissionValue { get; set; }
        public double PayableAmountSum { get; set; }
        public double PayableAmountValue { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }

        public string OwnerFullName { get; set; }
        public Int64 HotelRoomID { get; set; }
        public string EncryptChangeDate { get; set; }
        public int UserID { get; set; }
        public int FirmID { get; set; }
        public int ReservationCultureID { get; set; }
        public string ReservationCultureCode { get; set; }
        public string ReservationCultureSystemCode { get; set; }
        public string PenaltyRateName { get; set; }
        public int RefundableDayCount { get; set; }
        public string CancelPolicyDesc { get; set; }
        public string CancelPolicyText { get; set; }
        public string CancelPolicy { get; set; }
        public string EstimatedArrivalTime { get; set; }
        public string TravellerTypeName { get; set; }
        public string AccommodationName { get; set; }
        public string SalutationTypeName { get; set; }
        public int HotelID { get; set; }
        public string Property { get; set; }
        public int PeopleCount { get; set; }
        public int RoomCount { get; set; }
        public string CountryName { get; set; }
        public string HotelCityName { get; set; }
        public string HotelPostCode { get; set; }
        public int NightCount { get; set; }
        public int ID { get; set; }
        public string HotelName { get; set; }
        public string HotelEmail { get; set; }
        public string HotelPhone { get; set; }
        public string HotelAddress { get; set; }
        public Int64 ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        // public string ReservationDate { get; set; }
        public string FullName { get; set; }
        public string ReservationOwner { get; set; }
        public string GuestFullName { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }
        // public string CheckInDate { get; set; }

        // [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }
        // public string CheckOutDate { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string Rooms { get; set; }
        public string PayableAmount { get; set; }
        public string Commission { get; set; }
        public string Currencysymbol { get; set; }


        public string PropertyConditions { get; set; }
        public string EncryptReservationID { get; set; }

        public string Encryptcc { get; set; }

        public string Encrypthistory { get; set; }

        public int RoomIndex { get; set; }

        public string reservationOperationID { get; set; }

        public bool creditCardUsed { get; set; }
        public int statusID { get; set; }

        public bool lbtnCC { get; set; }

        public bool lbtnReportAsInvalidCC { get; set; }
        public bool lbtnCancel { get; set; }

        public bool lbtnMarkAsNoUse { get; set; }
        public bool lbtnChangeDate { get; set; }


        public string BedOptionNo { get; set; }

        public string Day { get; set; }

        public string MonthName { get; set; }

        public string CurrencySymbol { get; set; }

        public string RoomPrice { get; set; }
    }
}