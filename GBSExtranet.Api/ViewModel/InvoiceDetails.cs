using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//BalsTechnology-SK   
namespace GBSExtranet.Api.ViewModel
{
    public class InvoiceDetails
    {
        public string Period { get; set; }

        public string Amount { get; set; }

        public string InvoiceDate { get; set; }

        public string DueDate { get; set; }

        public string InvoiceID { get; set; }

        public string ReservationID { get; set; }

        public string ReservationOwnerFullName { get; set; }

        public string CurrencySymbol { get; set; }

        public string ComissionRate { get; set; }

        public string ComissionAmount { get; set; }

        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }

        public int ID { get; set; }

        public string FirmName { get; set; }

        public string ReservationDate { get; set; }

        public int PayableAmount { get; set; }

        public int GrossRevenue { get; set; }
        public int CommissionAmount { get; set; }

        public int Date { get; set; }
    }
}