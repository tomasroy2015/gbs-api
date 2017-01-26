using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GBSExtranet.Api.Models;
using GBSExtranet.Api.ServiceLayer;
using GBSExtranet.Api.ViewModel;
using System.Web;
using System.ServiceModel.Channels;
using Business;
using System.Data;
//BalsTechnology-SK   
namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class InvoiceController : ApiController
    {
        [Route("invoice/getinvoice")]
        [HttpGet]
        public HttpResponseMessage GetInvoices(string cultureCode, int offset)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new InvoiceService().GetInvoices(cultureCode, offset);
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("invoice/getpendinginvoice")]
        [HttpGet]
        public HttpResponseMessage GetpendingInvoices(string cultureCode, int offset)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new InvoiceService().GetpendingInvoices(cultureCode, offset);
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("invoice/getinvoicedetails")]
        [HttpGet]
        public HttpResponseMessage GetInvoiceDetails(string invoiceid, string cultureCode)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new InvoiceDetailService().GetInvoiceDetails(invoiceid, cultureCode);
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("invoice/getmonthlyrevenue")]
        [HttpGet]
        public HttpResponseMessage GetMonthlyRevenue(string Month, string Year, string cultureCode)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    string month = "12";
                    string year = "2015";
                    //string currentMonth = DateTime.Now.Month.ToString();
                    //string currentYear = DateTime.Now.Year.ToString();
                    var data = new InvoiceService().GetMonthlyRevenue(month, year, cultureCode);
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}