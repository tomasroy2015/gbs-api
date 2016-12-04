using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyFacilities
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Sort { get; set; }
        public bool Active { get; set; }
        public string Activity { get; set; }
        public string Service { get; set; }
        public string General { get; set; }
        public string AttributeHeaderName { get; set; }
        public int AttributeID { get; set; }
        public int UnitID { get; set; }
        public int CurrencyID { get; set; }
        public int HotelID { get; set; }
        public double PaidAmount { get; set; }
        public string AttributeName { get; set; }
        public bool? Charged { get; set; }
        public bool? Charged1 { get; set; }
        public bool hasAttribute { get; set; }
        public bool Chargable { get; set; }
    }
}