using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.Models
{
    public class RequestObject
    {
        public string Order { get; set; }
        public string OrderBy { get; set; }
        public int Length { get; set; }
        public int Offset { get; set; }
        public int Page { get; set; }
        public string Culture { get; set; }
        public Filter[] Filters { get; set; }
    }
}