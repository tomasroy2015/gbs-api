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
    public class UserOperationController : ApiController
    {
        [Route("userOperation/getAll")]
        [HttpPost]
        public HttpResponseMessage GetAll(RequestObject filter)
        {
            try
            {
                ApiResponseMessage message = new ApiResponseMessage();
                if (this.ModelState.IsValid)
                {
                    message.data = new UserOperationService().ReadAll(filter);
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
