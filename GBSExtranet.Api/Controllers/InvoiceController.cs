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
        public HttpResponseMessage GetInvoices(int hotelID,string cultureCode, int offset)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new InvoiceService().GetInvoices(hotelID,cultureCode, offset);
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
        public HttpResponseMessage GetMonthlyRevenue(int HotelID, string cultureCode)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    string currentMonth = DateTime.Now.Month.ToString();
                    string currentYear = DateTime.Now.Year.ToString();
                    var data = new InvoiceService().GetMonthlyRevenue(HotelID, currentMonth, currentYear, cultureCode);
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