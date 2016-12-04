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
    public class AuthorizationController : ApiController
    {
        [Route("authorize/getSecurityGroups")]
        [HttpGet]
        public HttpResponseMessage GetSecurityGroups(string  cultureCode) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new AuthorizationService().GetSecurityGroup(cultureCode);
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

        [Route("authorize/getSecurityGroupRights")]
        [HttpGet]
        public HttpResponseMessage GetSecurityGroupRights(string cultureCode,int securityGroupID,int offset)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new AuthorizationService().GetSecurityGroupRights(cultureCode, securityGroupID, offset);
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

        [Route("authorize/updateGroups/{securityGroupID}")]
        [HttpPost]
        public HttpResponseMessage Update(int securityGroupID, List<GBSExtranet.Api.ViewModel.Authorization> selectedItems) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new AuthorizationService().Update(securityGroupID, selectedItems);
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

        [Route("authorize/deleteGroups")]
        [HttpPost]
        public HttpResponseMessage Delete(int securityGroupID)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var data = new AuthorizationService().Delete(securityGroupID);
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
