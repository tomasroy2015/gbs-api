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
    public class InboxController : ApiController
    {
         [Route("inbox/getinboxmessage")]
         [HttpGet]
         public HttpResponseMessage GetInboxmessages(string ReceiverID, string cultureCode)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new InboxServices().GetInboxmessages(ReceiverID, cultureCode);
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
         [Route("inbox/deleteinboxmessage")]
         [HttpGet]
         public HttpResponseMessage DeleteInboxmessages(string ReceiverID,int MessageID, string cultureCode)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new InboxServices().DeleteInboxmessages(ReceiverID,MessageID, cultureCode);
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



         [Route("inbox/getuseremail")]
         [HttpGet]
         public HttpResponseMessage GetUserEmails(string cultureCode)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new InboxServices().GetUserEmails(cultureCode);
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
         [Route("inbox/sendmessage")]
         [HttpGet]
         public HttpResponseMessage Insertsendmessage(string SenderID,string ReceiverID,string Subject,string Message, string cultureCode)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new InboxServices().Insertsendmessage(SenderID, ReceiverID, Subject,Message, cultureCode);
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