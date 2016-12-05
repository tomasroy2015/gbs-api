using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GBSExtranet.Api.ViewModel;
using GBSExtranet.Repository;
using GBSExtranet.Api.ServiceLayer;
using System;
namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PropertyCancelController : ApiController
    {
        [Route("propertyCancelPolicy/getPropertyCancelPolicy")]
        [HttpGet]
        public HttpResponseMessage GetPropertyCancelPolicy(string culture,int hotelID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var policy = new PropertyCancelService().GetHotelCancelPolicy(culture).FirstOrDefault(f=>f.HotelID == hotelID);
                    //if (policy != null)
                    //{
                    //    var policyInfo = new PropertyCancelService().GetHotelCancelPolicyinfo(hotelID,culture);
                    //    return Request.CreateResponse(HttpStatusCode.OK, policyInfo);
                    //}
                    return Request.CreateResponse(HttpStatusCode.OK, policy);
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

        [Route("propertyCancelPolicy/updatePropertyCancelPolicy/{userID}")]
        [HttpPost]
        public HttpResponseMessage UpdatePropertyCancelPolicy(PropertyCancelPolicy model,int userID) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PropertyCancelService().UpdatePropertyCancelPolicy(model, userID);
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

        [Route("propertyCancelPolicy/getPrepaymentNames")]
        [HttpGet]
        public HttpResponseMessage GetPrepaymentNames(string culture)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new PropertyCancelService().GetPrepaymentNames(culture);
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
