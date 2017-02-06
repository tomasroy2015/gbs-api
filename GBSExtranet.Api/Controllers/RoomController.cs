//Balstechnology-AJ

using GBSExtranet.Api.ServiceLayer;
using GBSExtranet.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GBSExtranet.Api.Controllers
{
     [EnableCors("*", "*", "*")]
    public class RoomController : ApiController
    {


         [Route("roomdetails/getroom")]
         [HttpGet]
         public HttpResponseMessage GetRoomDetails(int hotelID, string cultureCode)
         {

             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new RoomServices().GetRoomDetails(hotelID, cultureCode);
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


   
