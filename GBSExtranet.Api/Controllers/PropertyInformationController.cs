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
namespace GBSExtranet.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PropertyInformationController : ApiController
    {
         [Route("propertyInformation/getHotelInformation")]
         [HttpGet]
         public HttpResponseMessage GetHotelInformation(string culture,Int64? hotelID)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var message = new PropertyOperationService().GetHotels(culture, hotelID);
                     return Request.CreateResponse(HttpStatusCode.OK, message);
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

         [Route("propertyInformation/updatePropertyInfo")]
         [HttpPost]
         public HttpResponseMessage UpdatePropertyInfo(Hotel PropertyInfo)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var message = new PropertyOperationService().UpdateProperty(PropertyInfo);
                     return Request.CreateResponse(HttpStatusCode.OK, message);
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
