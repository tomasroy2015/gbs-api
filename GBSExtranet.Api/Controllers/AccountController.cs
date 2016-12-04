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
    public class AccountController : ApiController
    {
        /// <summary>
        /// user login. 
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns>Return all userdata if login successful</returns>
        [Route("user/login/")]
        [HttpGet]

        public HttpResponseMessage Login(string Email, string Password,string cultureCode)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    string userIpAddress = GetClientIp(null);
                    var user = new AccountService().GetLoginUser(Email, Password, cultureCode, ref userIpAddress);
                   
                    if (user != null){
                        if (user.Locked == true)
                        {
                            string msg = "Your can't log on the System, your status is Locked. Please contact your Department Administrator.";
                            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, msg);
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, user);
                    }                    
                    else {
                        string message = "Please check your username and password.";
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, message);
                    }
                    
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
        /// <summary>
        /// An email will be sent to user's email address with password reset instruction.
        /// </summary>
        /// <param name="resetModel"></param>
        [Route("user/resetpassword")]
        [HttpPost]
        public HttpResponseMessage ResetPassword(ResetPassword reset)
        {
            try
            {
                var user = new AccountService().ResetPassword(reset);     
                return Request.CreateResponse(HttpStatusCode.OK, true);;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Password will be reset
        /// </summary>
        /// <param name="resetModel"></param>
        [Route("user/updatePassword")]
        [HttpPost]
        public HttpResponseMessage SetNewPassword(ResetPassword reset) 
        {
            try
            {
                new AccountService().UpdatePassword(reset);

                var response = Request.CreateResponse(HttpStatusCode.OK, true);
                return response;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }    
          private string GetClientIp(HttpRequestMessage request = null)
          {
                request = request ?? Request;

                if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                      return   ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
                else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                     RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                     return prop.Address;
                }
                else if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                      return null;
                }
           }
    }
}
