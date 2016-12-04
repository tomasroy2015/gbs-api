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

namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*","*","*")]
    public class PropertyReviewController : ApiController
    {
        [Route("propertyReview/getPropertyReviews")]
        [HttpGet]
        public HttpResponseMessage GetPropertyReviews(int hotelID, string culture)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PropertyReviewService().GetReviews(hotelID, culture);
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
        [Route("propertyReview/getIndividualReviews")]
        [HttpGet]
        public HttpResponseMessage GetIndividualReviews(int hotelID, string culture)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PropertyReviewService().GetIndividualReviews(hotelID, culture); 
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
    }
}
