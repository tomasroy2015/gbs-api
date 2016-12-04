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
    public class PropertyFacilitiesController : ApiController
    {
         [Route("propertyFacilities/getPropertyFacilities")]
         [HttpGet]
         public HttpResponseMessage GetPropertyFacilities(int hotelID,string culture) 
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var attributeHeader = new PropertyFacilitiesService().GetPropertyFacilitiesHeader(hotelID,culture);
                     if (attributeHeader != null && attributeHeader.Count > 0)
                     {
                         foreach (var header in attributeHeader)
                         {
                             header.PropertyFacilitiesItems = new PropertyFacilitiesService().GetHotelAttributes(header.ID, hotelID,culture); 
                         }
                     }
                     return Request.CreateResponse(HttpStatusCode.OK, attributeHeader);
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

         [Route("propertyFacilities/savePropertyFacilities/{hotelID:int}/{culture}")]
         [HttpPost]
         public HttpResponseMessage SavePropertyFacilities(List<PropertyFacilities>facilities, int hotelID,string culture)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var response = new PropertyFacilitiesService().SavePropertyFacility(facilities, hotelID, culture);

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
