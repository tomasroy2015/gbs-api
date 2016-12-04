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
    public class DropdownController : ApiController
    {
         [Route("dropdown/getCurrencies")]
         [HttpGet]
         public HttpResponseMessage GetCurrencies(string Culture)
         {
             try
             {
                 ApiResponseMessage message = new ApiResponseMessage();
                 if (this.ModelState.IsValid)
                 {
                     message.data = new DropdownService().ReadCurrencies(Culture);
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
         [Route("dropdown/getUnit")]
         [HttpGet]
         public HttpResponseMessage GetUnit(string cultureCode)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().ReadUnit(cultureCode);
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

         [Route("dropdown/getCountries")]
         [HttpGet]
         public HttpResponseMessage GetCountries(string Culture) 
         {
             try
             {
                 ApiResponseMessage message = new ApiResponseMessage();
                 if (this.ModelState.IsValid)
                 {
                     message.data = new DropdownService().ReadCountries(Culture);
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
         [Route("dropdown/getRegions")]
         [HttpGet]
         public HttpResponseMessage GetRegions(string Culture)
         {
             try
             {
                 ApiResponseMessage message = new ApiResponseMessage();
                 if (this.ModelState.IsValid)
                 {
                     message.data = new DropdownService().ReadRegions(Culture);
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


         [Route("dropdown/getChildrenUnit")]
         [HttpGet]
         public HttpResponseMessage GetChildrenUnit(string culture) 
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().ReadChildrenPolicyUnits(culture);
                     return Request.CreateResponse(HttpStatusCode.OK, data);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }

             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }

         [Route("dropdown/getPenaltyRate")]
         [HttpGet]
         public HttpResponseMessage GetPenaltyRate(string culture,int rateTypeID)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().GetPenaltyRate(culture, rateTypeID);
                     return Request.CreateResponse(HttpStatusCode.OK, data);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }

             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }

         [Route("dropdown/getPriceUnits")]
         [HttpGet]
         public HttpResponseMessage GetPriceUnits(string culture)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().GetPriceUnits(culture);
                     return Request.CreateResponse(HttpStatusCode.OK, data);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }

             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }

         [Route("dropdown/getYears")]
         [HttpGet]
         public HttpResponseMessage GetYears(string culture) 
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().GetYears();
                     return Request.CreateResponse(HttpStatusCode.OK, data);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }

             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }

         [Route("dropdown/getRoomsByHotel")]
         [HttpGet]
         public HttpResponseMessage GetRoomsByHotel(int hotelID,string culture)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().GetHotelRooms(hotelID, culture);
                     return Request.CreateResponse(HttpStatusCode.OK, data);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }

             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }

         [Route("dropdown/getPricePolicy")]
         [HttpGet]
         public HttpResponseMessage GetPricePolicy(string culture)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().GetPricePlicy(culture);
                     return Request.CreateResponse(HttpStatusCode.OK, data);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }

             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }

         [Route("dropdown/getAccomodationByID")]
         [HttpGet]
         public HttpResponseMessage GetAccomodationByID(int Id,string culture)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().GetTypeHotelAccommodationByID(Id, culture);
                     return Request.CreateResponse(HttpStatusCode.OK, data);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }

             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }

         [Route("dropdown/fillUnitList")]
         [HttpGet]
         public HttpResponseMessage FillUnitList(int startIndex, int endIndex)
         {
             try
             {
                 if (this.ModelState.IsValid)
                 {
                     var data = new DropdownService().FillUnitList(startIndex,endIndex);
                     return Request.CreateResponse(HttpStatusCode.OK, data);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }

             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }
    }
}
