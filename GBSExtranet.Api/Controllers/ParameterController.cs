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
    public class ParameterController : ApiController
    {
        [Route("parameter/getParameters")]
        [HttpPost]
        public HttpResponseMessage GetParameter(RequestObject filter)
        {
            try
            {
                ApiResponseMessage message = new ApiResponseMessage();
                if (this.ModelState.IsValid)
                {
                    message.data = new ParameterService().ReadAll(filter);
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

        [Route("parameter/addParameter")]
        [HttpPost]
        public HttpResponseMessage AddParameter(Parameter model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new ParameterService().Create(model);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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

        [Route("parameter/editParameter")]
        [HttpPost]
        public HttpResponseMessage EditParameter(Parameter model)
        {
            try 
            {
                if (this.ModelState.IsValid)
                {
                    var result = new ParameterService().Update(model); 
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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

        [Route("parameter/deleteParameter")]
        [HttpPost]
        public HttpResponseMessage DeleteParameter(Parameter model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new ParameterService().Delete(model);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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

        [Route("parameter/deleteParameters")]
        [HttpPost]
        public HttpResponseMessage DeleteParameters(string[] ids)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new ParameterService().DeleteParameters(ids);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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
