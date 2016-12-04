using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.Models
{
    public class Filter
    {
        public string condition { get; set; }
        public string label { get; set; }
        public string slugRequest { get; set; }
        public string model { get; set; }
        public object value { get; set; }
        
    }
}