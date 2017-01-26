using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class Reservation
    {
        public Int64 ReservationID { get; set; }
        public string PinCode { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationOwner { get; set; }
        public string ReservationName { get; set; } 
        public string Sum { get; set; }
        public double PayableAmount { get; set; }
        public string Cost { get; set; }
        public string Deposit { get; set; }
        public double ChargedAmount { get; set; }
        public string StatusName { get; set; }
        public int StatusID { get; set; }
        public int ReservationOperationID { get; set; }

        public string ReservationOperation { get; set; }
        public string EncryptReservationID { get; set; }
        public string Encryptcc { get; set; }
        public string Encrypthistory { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int CCTypeID { get; set; }
        public string CreditCardProvider { get; set; }
        public string NameontheCreditCard { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVCCode { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ComissionRate { get; set; }

        public string ActualAmount { get; set; }

        public string ComissionAmount { get; set; }

        public string PayableAmounts { get; set; }
    }
}