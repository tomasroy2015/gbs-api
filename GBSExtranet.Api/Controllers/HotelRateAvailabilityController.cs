using System;
using System.Collections.Generic;
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
using System.Collections;

namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class HotelRateAvailabilityController : ApiController
    {
        
        [Route("hotelRateAvailability/getHotelRateOverview")]
        [HttpGet]
        public HttpResponseMessage GetHotelRateAvailability(string culture, int hotelID, string startDate, string endDate)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new HotelRateAvailabilityService().HotelRateAndAvailability(hotelID, culture,Convert.ToDateTime(startDate),Convert.ToDateTime(endDate));
                   
                    //HttpContext.Current.Session["DateAvailability"] = new HotelRateAvailabilityService().GetDateAvailability();
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("hotelRateAvailability/closeOpenAvailability")] 
        [HttpPost]
        public HttpResponseMessage CloseOpenAvailability(Hashtable dateAvailability, string dateValue, int hotelID, string sessionID, string isClosed, string roomID) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                   // Hashtable availability = (Hashtable)HttpContext.Current.Session["DateAvailability"];
                    var response = new HotelRateAvailabilityService().CloseOpenAvailabilityRefresh(dateValue, hotelID, sessionID, isClosed, roomID);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
