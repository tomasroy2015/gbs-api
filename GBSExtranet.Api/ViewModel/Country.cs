using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CultureCode { get; set; }
        public int CurrencyID { get; set; }
        public string Currency { get; set; }
        public string VAT { get; set; }
        public bool CityTax { get; set; }
        public long HitCount { get; set; }
        public short Sort { get; set; }
        public bool Active { get; set; }
        public string TemporaryCode { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OptUserID { get; set; }
    }
}