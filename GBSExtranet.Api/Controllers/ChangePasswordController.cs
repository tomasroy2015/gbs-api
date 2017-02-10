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
    public class ChangePasswordController : ApiController
    {
          [Route("password/ChangePassword")]
          [HttpGet]
          public HttpResponseMessage UpdatePassword(string UserID, string CurrentPassword, string NewPassword, string cultureCode)
          {
              try
              {
                  if (this.ModelState.IsValid)
                  {
                      var encryptedCurrentPass = new BizCrypto.AES128().Encrypt(CurrentPassword);
                      var encryptedNewPass = new BizCrypto.AES128().Encrypt(NewPassword);

                      var data = new ChangePasswordServices().UpdatePassword(UserID, encryptedCurrentPass, encryptedNewPass, cultureCode);
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