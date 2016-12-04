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
     [EnableCors("*", "*", "*")]
    public class PropertyPolicyController : ApiController
    {
         [Route("propertyPolicies/getPropertyPolicies")]
         [HttpGet]
         public HttpResponseMessage GetPropertyPolicies(string culture, int hotelID)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var response = new PropertyPolicyService().GetPropertyPolicies(culture, hotelID);
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

         [Route("propertyPolicies/getPropertyPolicySummary")]
         [HttpGet]
         public HttpResponseMessage GetPropertyPolicySummary(int hotelID)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var response = new PropertyPolicyService().GetPolicySummaries(hotelID);
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

         [Route("propertyPolicies/updatePropertyPolicies/{culture}/{hotelID}/{userID}/{currencyID}")]
         [HttpPost]
         public HttpResponseMessage UpdatePropertyPolicies(List<PropertyPolicies> policies, string culture, int hotelID,long userID,int currencyID)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var response = new PropertyPolicyService().UpdatePropertyPolicies(policies, culture, hotelID, userID, currencyID);
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
