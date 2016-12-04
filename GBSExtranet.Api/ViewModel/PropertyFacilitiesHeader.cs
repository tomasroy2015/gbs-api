using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyFacilitiesHeader
    {
        public int ID { get; set; }
        public string AttributeHeaderCode { get; set; }
        public string AttributeHeaderName { get; set; }
        public List<PropertyFacilities> PropertyFacilitiesItems { get; set; }
    }
}