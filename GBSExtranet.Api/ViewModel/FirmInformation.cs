using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class FirmInformation
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string TaxOffice { get; set; }

        public string TaxID { get; set; }

        public string ExecutiveName { get; set; }

        public string ExecutiveSurname { get; set; }
    }
}