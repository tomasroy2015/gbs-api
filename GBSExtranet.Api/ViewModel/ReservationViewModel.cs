using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class ReservationViewModel
    {
        public List<PropertyReservation> ReservationDetails { get; set; }
        public PropertyReservation ReservationDetail { get; set; }
        public string Promotions { get; set; }
    }
}