using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyStatistics
    {
        public string PartID { get; set; }
        public string RecordID { get; set; }
        public string HitCount { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string MonthName { get; set; }
        public string Day { get; set; }
        public string DayName { get; set; }
        public string Date { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ReservationCount { get; set; }
    }
}