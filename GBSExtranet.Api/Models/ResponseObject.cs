using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.ServiceModel;
using GBSExtranet.Api.ViewModel;
using Business;
using System.Data;

namespace GBSExtranet.Api.Models
{
    public class ResponseObject
    {
        public List<object> rows { get; set; } 
        public Int32 totalRows { get; set; }
    }
}