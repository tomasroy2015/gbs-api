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
    public class RegionController : ApiController
    {
        [Route("region/getRegions")]
        [HttpPost]
        public HttpResponseMessage GetRegions(RequestObject filter) 
        {
            try
            {
                ApiResponseMessage message = new ApiResponseMessage();
                if (this.ModelState.IsValid)
                {
                    message.data = new RegionService().ReadAll(filter);
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
        [Route("region/addRegion")]
        [HttpPost]
        public HttpResponseMessage AddRegion(Region region)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new RegionService().Create(region);
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

        [Route("region/editRegion")]
        [HttpPost]
        public HttpResponseMessage EditRegion(Region region)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new RegionService().Edit(region);
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

        [Route("region/deleteRegion")]
        [HttpPost]
        public HttpResponseMessage DeleteRegion(Region model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new RegionService().Delete(model);
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
        [Route("region/deleteRegions")]
        [HttpPost]
        public HttpResponseMessage DeleteRegion(string[] ids)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new RegionService().DeleteRegions(ids);
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
