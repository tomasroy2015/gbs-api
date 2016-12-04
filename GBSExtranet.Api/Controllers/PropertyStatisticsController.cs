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
using System.Globalization;

namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*","*","*")]
    public class PropertyStatisticsController : ApiController
    {
        [Route("propertyStatistics/dailyStatistics")]
        [HttpGet]
        public HttpResponseMessage DisplayPropertyStatistics(string culture,string startDate,string endDate, int hotelID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PropertyStatisticService().DisplayPropertyStatistics(culture,startDate,endDate, hotelID);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
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
        [Route("propertyStatistics/monthlyStatistics")]
        [HttpGet]
        public HttpResponseMessage DisplayPropertyStatistics(string culture, string year, int hotelID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PropertyStatisticService().MonthlyPropertyStatistics(culture, year, hotelID);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
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

        [Route("propertyStatistics/yearlyStatistics")]
        [HttpGet]
        public HttpResponseMessage DisplayPropertyStatistics(string culture, int hotelID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PropertyStatisticService().YearlyPropertyStatistics(culture, hotelID);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
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

        [Route("propertyStatistics/validateDate")]
        [HttpGet]
        public HttpResponseMessage CheckDateDiff(string StartDate, string Enddate)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            double DateDiff1;
            try
            {
                if (StartDate.Contains('.'))
                {
                    dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (Enddate.Contains('.'))
                {
                    dtEnd = DateTime.ParseExact(Enddate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    dtEnd = DateTime.ParseExact(Enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                //  DateTime TodayDate = DateTime.Now;
                DateDiff1 = (dtEnd - dtStart).TotalDays;
            }
            catch (Exception ex)
            {
                string hostName1 = Dns.GetHostName();
                string GetUserIPAddress = Dns.GetHostByName(hostName1).AddressList[0].ToString();
               
                //string GetUserIPAddress = GetUserIPAddress1();
                using (BaseService baseRepo = new BaseService())
                {
                    //BizContext BizContext1 = new BizContext();
                    BizApplication.AddError(baseRepo._context,"Statistics", ex.Message, ex.StackTrace, DateTime.Now, GetUserIPAddress);
                }
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, DateDiff1);
        }
    }
}
