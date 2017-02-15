using GBSExtranet.Api.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GBSExtranet.Api.Controllers
{
     [EnableCors("*", "*", "*")]
    public class AddonservicesController : ApiController
    {
         [Route("addonservices/insertaddondetails")]
         [HttpPost]
         public HttpResponseMessage insertaddondetails(string HotelID, string Title, string Price, string Changetype)
         {
              AddonService obj=new AddonService();
             HotelID=Convert.ToString(obj.insertaddondetails(HotelID,Title,Price,Changetype));
             return Request.CreateResponse(HttpStatusCode.OK, HotelID);
         }
         [Route("addonservices/Displayaddons")]
         [HttpGet]
         public HttpResponseMessage Displayaddons()
         {
             AddonService obj = new AddonService();
             var displayDetails = obj.Displayaddonsdetails();
             return Request.CreateResponse(HttpStatusCode.OK, displayDetails);
             //return null;
         }
         [Route("addonservices/drpchangetype")]
         [HttpGet]
         public HttpResponseMessage drpchangetype()
         {
             AddonService obj = new AddonService();
             var displayDetails = obj.Displaydrpchangetype();
             return Request.CreateResponse(HttpStatusCode.OK, displayDetails);
             //return null;
         }
         [Route("addonservices/Deleteaddons")]
         [HttpPost]
         public HttpResponseMessage Deleteaddons(int Id)
         {
             AddonService obj = new AddonService();
             var Id1 = obj.Deleteaddons(Id);
             return Request.CreateResponse(HttpStatusCode.OK, Id1);
         }
    }
}