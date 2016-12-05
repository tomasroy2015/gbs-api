using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GBSExtranet.Api.ViewModel;
using GBSExtranet.Repository;
namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*","*","*")]
    public class ChildrenPoliciesController : ApiController
    {
        [Route("childrenPolicies/getChildrenPolicies/{hotelID}")]
        [HttpGet]
        public HttpResponseMessage GetChildrenPolicy(int hotelID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new ChildrenPolicyService().GetChildrenPolicy(hotelID);
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

        [Route("childrenPolicies/getChildrenPolicySettings")]
        [HttpGet]
        public HttpResponseMessage GetChildrenPolicy(int hotelID,string culture)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new ChildrenPolicyService().GetChildrenPolicySettings(hotelID, culture);
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

        [Route("childrenPolicies/getChildrenPolicySummary")]
        [HttpGet]
        public HttpResponseMessage GetChildrenPolicySummary(string currencyCode,int? hotelID, string culture) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new ChildrenPolicyService().GetChildrenPolicySummary(currencyCode,hotelID, culture);
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

        [Route("childrenPolicies/updateChildrenPolicies/{hotelID}/{currencyID}")]
        [HttpPost]
        public HttpResponseMessage UpdateChildrenPolicies(List<ChildrenPolicy> model, int? hotelID, int? currencyID) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = new ChildrenPolicyService().UpdateChildrenPolicy(model, hotelID, currencyID);
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
