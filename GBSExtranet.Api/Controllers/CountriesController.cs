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
    public class CountriesController : ApiController
    {
        [Route("country/getCountries")]
        [HttpPost]
        public HttpResponseMessage GetCountries(RequestObject filter)
        {
            try
            {
                ApiResponseMessage message =new ApiResponseMessage();
                if (this.ModelState.IsValid)
                {
                    message.data = new CountryService().ReadAll(filter);
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

        [Route("country/addCountry")]
        [HttpPost]
        public HttpResponseMessage AddCountry(Country country) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new CountryService().Create(country);
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

        [Route("country/editCountry")]
        [HttpPost]
        public HttpResponseMessage EditCountry(Country country)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new CountryService().Edit(country);
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

        [Route("country/deleteCountry")]
        [HttpPost]
        public HttpResponseMessage DeleteCountry(Country model) 
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new CountryService().Delete(model);
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

        [Route("country/deleteCountries")]
        [HttpPost]
        public HttpResponseMessage DeleteCountry(string[] ids)  
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = new CountryService().DeleteCountries(ids);
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
