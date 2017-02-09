using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Cors;
using GBSExtranet.Api.Models;
using GBSExtranet.Api.ServiceLayer;
using GBSExtranet.Api.ViewModel;
using System.Web;
using System.ServiceModel.Channels;
using Business;
using System.Data;
using System.Net;

namespace GBSExtranet.Api.Controllers
{
     [EnableCors("*", "*", "*")]
    public class FirmInformationController : ApiController
    {

         [Route("firm/getfirminformation")]
         [HttpGet]
         public HttpResponseMessage GetFirmInformation(string FirmID, string cultureCode)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new FirmInformationService().GetFirmInformation(FirmID, cultureCode);
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