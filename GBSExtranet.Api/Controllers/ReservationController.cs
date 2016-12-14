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

namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ReservationController : ApiController
    {
        [Route("reservation/getReservationByProperty")]
        [HttpGet]
        public HttpResponseMessage GetReservationByProperty(int hotelID, string cultureCode, int offset)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyReservationService().GetReservationByProperty(hotelID, cultureCode,offset);
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

        [Route("reservation/getReservationDetails")]
        [HttpGet]
        public HttpResponseMessage GetReservationByDetails(long id, string culture) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyReservationService().GetReservationsForView(id, culture);
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

        [Route("reservation/getReservationPromotions")]
        [HttpGet]
        public HttpResponseMessage GetReservationPromotions(int reservationID,string culture) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new PropertyReservationService().ReservationPromotions(reservationID, culture);
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
